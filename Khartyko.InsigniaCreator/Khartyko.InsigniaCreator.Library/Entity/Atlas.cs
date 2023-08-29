/** \addtogroup Library
 * @{
 */
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Interfaces;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

/// <summary>
/// 
/// </summary>
public class Atlas : IEntity
{
	private string _name;
    private double _width;
    private double _height;
    private RgbColor _backgroundColor;

    /// <summary>
    /// Gets the unique long to identify an Atlas.
    /// </summary>
    public long Id { get; }
    
    /// <summary>
    /// Gets the list of Cartographs associated with an Atlas.
    /// </summary>
    public IList<Cartograph> Cartographs { get; }
    
    /// <summary>
    /// Gets or Sets the name of an Atlas.
    /// </summary>
    /// <exception cref="ArgumentNullException">Can be thrown if 'value' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'value' is empty or whitespace.</exception>
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
    /// Gets or Sets the width of an Atlas.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is 0.0 or negative.</exception>
    public double Width 
    {
        get => _width;
        set
        {
            AssertionHelper.PositiveCheck(value, nameof(value));
            
            _width = value;
        }
    }
    
    /// <summary>
    /// Gets or Sets the height of an Atlas.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'value' is 0.0 or negative.</exception>
    public double Height 
    {
        get => _height;
        set
        {
            AssertionHelper.PositiveCheck(value, nameof(value));

            _height = value;
        }
    }
    
    /// <summary>
    /// Gets or Sets the background color of an Atlas.
    /// </summary>
    public RgbColor BackgroundColor 
    {
        get => _backgroundColor;
        set
        {
            AssertionHelper.NullCheck(value, nameof(value));

            _backgroundColor = value;
        }
    }
    
    /// <summary>
    /// Constructs an Atlas with the given data.
    /// 'Cartographs' will be set to an empty list.
    /// </summary>
    /// <param name="id">The unique identifier to the Atlas.</param>
    /// <param name="name">The name of the Atlas.</param>
    /// <param name="width">The width of the Atlas.</param>
    /// <param name="height">The height of the Atlas.</param>
    /// <param name="backgroundColor">The background color of the Atlas.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'name' or 'backgroundColor' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'name' is empty or whitespace.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if either 'width' or 'height' is 0.0 or negative.</exception>
    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor)
    {
        AssertionHelper.PositiveCheck(id, nameof(id));
        AssertionHelper.EmptyOrWhitespaceCheck(name, nameof(name));
        AssertionHelper.PositiveCheck(width, nameof(width));
        AssertionHelper.PositiveCheck(height, nameof(height));
        AssertionHelper.NullCheck(backgroundColor, nameof(backgroundColor));
        
        Id = id;
        _name = name;
        _width = width;
        _height = height;
        _backgroundColor = backgroundColor;

        Cartographs = new List<Cartograph>();
    }

    /// <summary>
    /// Constructs an Atlas with the given data.
    /// </summary>
    /// <param name="id">The unique identifier to the Atlas.</param>
    /// <param name="name">The name of the Atlas.</param>
    /// <param name="width">The width of the Atlas.</param>
    /// <param name="height">The height of the Atlas.</param>
    /// <param name="backgroundColor">The background color of the Atlas.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'name', 'backgroundColor', or 'cartograph' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if 'name' is empty or whitespace.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if either 'width' or 'height' is 0.0 or negative.</exception>
    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor, Cartograph cartograph)
        : this(id, name, width, height, backgroundColor)
    {
        AssertionHelper.NullCheck(cartograph, nameof(cartograph));
        
        Cartographs.Add(cartograph);
    }

    /// <summary>
    /// Constructs an Atlas with the given data.
    /// </summary>
    /// <param name="id">The unique identifier to the Atlas.</param>
    /// <param name="name">The name of the Atlas.</param>
    /// <param name="width">The width of the Atlas.</param>
    /// <param name="height">The height of the Atlas.</param>
    /// <param name="backgroundColor">The background color of the Atlas.</param>
    /// <exception cref="ArgumentNullException">Can be thrown if either 'name', 'backgroundColor', or 'cartographs' is null.</exception>
    /// <exception cref="ArgumentException">Can be thrown if either 'name' or 'cartographs' is empty or whitespace.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if either 'width' or 'height' is 0.0 or negative.</exception>
    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor, IList<Cartograph> cartographs)
        : this(id, name, width, height, backgroundColor)
    {
        AssertionHelper.EmptyCheck(cartographs, nameof(cartographs));
        AssertionHelper.DuplicatesCheck(cartographs, nameof(cartographs));

        cartographs.ToList().ForEach(cartograph =>
        {
            AssertionHelper.NullCheck(cartograph, nameof(cartographs));

            Cartographs.Add(cartograph);
        });
    }

    /// <summary>
    /// Constructs a copy of an existing Atlas.
    /// </summary>
    /// <param name="id">The unique identifier of the new Atlas.</param>
    /// <param name="existing">The existing Atlas to duplicate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Can be thrown if 'id' is negative.</exception>
    /// <exception cref="ArgumentNullException">Can be thrown if 'existing' is null.</exception>
    public Atlas(long id, Atlas existing)
    {
        AssertionHelper.PositiveCheck(id, nameof(id));
        AssertionHelper.NullCheck(existing, nameof(existing));
        
        Id = id;
        _name = existing.Name;
        _width = existing.Width;
        _height = existing.Height;
        _backgroundColor = existing.BackgroundColor;

        Cartographs = new List<Cartograph>(existing.Cartographs);
    }

    /// <summary>
    /// An override of the default Equals method that checks if the object in question has the same data as this Atlas instance.
    /// </summary>
    /// <remarks>
    /// If the object is null, it'll return false.
    /// If the object is 'this', it'll return true.
    /// Otherwise, the values are compared outright.
    /// </remarks>
    /// <param name="obj">The object that is compared against this instance of an Atlas.</param>
    /// <returns>A boolean value as to whether 'obj' is equal to this instance of an Atlas.</returns>
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

        return obj is Atlas atlas
               && atlas.Name.Equals(Name)
               && atlas.Width.Equals(Width)
               && atlas.Height.Equals(Height)
               && atlas.BackgroundColor.Equals(BackgroundColor)
               && atlas.Cartographs.All(Cartographs.Contains);
    }
}
/** @} */