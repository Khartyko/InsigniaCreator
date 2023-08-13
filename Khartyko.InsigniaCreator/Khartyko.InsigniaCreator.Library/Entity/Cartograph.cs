using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;
namespace Khartyko.InsigniaCreator.Library.Entity;

public class Cartograph : IIdBearer
{
	private string _name;
	public long Id { get; }
	public long AtlasId { get; }

	public bool IsActive { get; set; }

	public string Name
	{
		get => _name;
		set
		{
			StringHelper.EmptyOrWhitespaceCheck(value, nameof(value));
			 
			_name = value;
		}
	}

	public TemplateNetwork Template { get; }
	public ActiveNetwork Active { get; }

	public Cartograph(long id, long atlasId, string name, TemplateNetwork templateNetwork)
	{
		Id = id;
		AtlasId = atlasId;
		Name = name;
		Template = templateNetwork;

		IsActive = true;
		Active = new ActiveNetwork();
	}

	public Cartograph(long id, Cartograph existing)
	{
		ObjectHelper.NullCheck(existing, nameof(existing));
		
		Id = id;
		AtlasId = existing.AtlasId;
		Name = existing.Name;
		IsActive = existing.IsActive;
		Template = new TemplateNetwork(existing.Template);
		Active = new ActiveNetwork(existing.Active);
	}

	public bool Activate(Vector2 position)
	{
		ObjectHelper.NullCheck(position, nameof(position));
		
		if (!IsActive)
		{
			return false;
		}
        
		var result = false;
        
		Node? node = Template.GetNode(position);

		if (node is not null)
		{
			result = Active.Activate(node);
		}

		return result;
	}
}