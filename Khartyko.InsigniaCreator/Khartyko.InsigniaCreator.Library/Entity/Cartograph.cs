/** \addtogroup Library
 * @{
 */

using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Interfaces;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// Represents a uniquely-identified object that manages both an Active and Template Network.
/// </summary>
public class Cartograph : IEntity
{
	private string _name;
	
	/// <summary>
	/// Gets the unique long identifier of a Cartograph.
	/// </summary>
	public long Id { get; }
	
	/// <summary>
	/// Gets the identifier of the Atlas that a Cartograph belongs to.
	/// </summary>
	public long AtlasId { get; }

	/// <summary>
	/// Get or Sets the active state of a Cartograph.
	/// </summary>
	public bool IsActive { get; set; }

	/// <summary>
	/// Gets or Sets the name of a Cartograph.
	/// </summary>
	public string Name
	{
		get => _name;
		set
		{
			AssertionHelper.EmptyOrWhitespaceCheck(value, nameof(value));
			 
			_name = value;
		}
	}

	/// <summary>
	/// Gets the Template Network of a Cartograph.
	/// This has all the Nodes, Links, and Cells that could possibly be activated.
	/// </summary>
	public TemplateNetwork Template { get; }
	
	/// <summary>
	/// Gets the Active Network of a Cartograph.
	/// </summary>
	public ActiveNetwork Active { get; }

	/// <summary>
	/// Constructs a Cartograph with the given data.
	/// </summary>
	/// <param name="id">The unique identifier of the Cartograph.</param>
	/// <param name="atlasId">The unique identifier of the Atlas the Cartograph is associated with.</param>
	/// <param name="name">The name of the Cartograph.</param>
	/// <param name="templateNetwork">The Template Network of the Cartograph.</param>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if either 'id' or 'atlasId' is negative</exception>
	/// <exception cref="ArgumentNullException">Can be thrown if either 'name' or 'templateNetwork' is null.</exception>
	/// <exception cref="ArgumentException">Can be thrown if 'name' is either empty or whitespace.</exception>
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

	/// <summary>
	/// Constructs a copy of an existing Cartograph.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="existing"></param>
	/// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'id' is negative</exception>
	/// <exception cref="ArgumentNullException">Can be thrown if either 'existing' is null.</exception>
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

	/// <summary>
	/// Activates a Node with the given position, if it is present and hasn't already activated.
	/// </summary>
	/// <param name="position">The position of the Node to activate.</param>
	/// <exception cref="ArgumentNullException">Can be thrown if 'position' is null.</exception>
	/// <returns>A boolean result the denotes whether the specified Node was activated or not.</returns>
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

	/// <summary>
	/// An override of the default Equals method that checks if the object in question has the same data as this Cartograph instance.
	/// </summary>
	/// <remarks>
	/// If the object is null, it'll return false.
	/// If the object is 'this', it'll return true.
	/// Otherwise, the values are compared outright.
	/// </remarks>
	/// <param name="obj">The object that is compared against this instance of an Cartograph.</param>
	/// <returns>A boolean value as to whether 'obj' is equal to this instance of an Cartograph.</returns>
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

/** @} */