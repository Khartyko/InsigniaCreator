using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Interfaces;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Cartograph : IEntity
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
			AssertionHelper.EmptyOrWhitespaceCheck(value, nameof(value));
			 
			_name = value;
		}
	}

	public TemplateNetwork Template { get; }
	public ActiveNetwork Active { get; }

	public Cartograph(long id, long atlasId, string name, TemplateNetwork templateNetwork)
	{
		AssertionHelper.PositiveCheck(id, nameof(id));
		AssertionHelper.PositiveCheck(atlasId, nameof(atlasId));
		AssertionHelper.EmptyOrWhitespaceCheck(name, nameof(name));
		AssertionHelper.NullCheck(templateNetwork, nameof(templateNetwork));
		
		Id = id;
		AtlasId = atlasId;
		_name = name;
		Template = templateNetwork;

		IsActive = true;
		Active = new ActiveNetwork();
	}

	public Cartograph(long id, Cartograph existing)
	{
		AssertionHelper.PositiveCheck(id, nameof(id));
		AssertionHelper.NullCheck(existing, nameof(existing));
		
		Id = id;
		AtlasId = existing.AtlasId;
		_name = existing.Name;
		IsActive = existing.IsActive;
		Template = new TemplateNetwork(existing.Template);
		Active = new ActiveNetwork(existing.Active);
	}

	public bool Activate(Vector2 position)
	{
		AssertionHelper.NullCheck(position, nameof(position));
		
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

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj))
		{
			return false;
		}

		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		return obj is Cartograph cartograph
		       && cartograph.Name.Equals(Name)
		       && cartograph.Template.Equals(Template)
		       && cartograph.Active.Equals(Active);
	}
}