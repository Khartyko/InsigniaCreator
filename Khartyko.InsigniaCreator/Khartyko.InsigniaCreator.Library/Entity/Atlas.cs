using System.Reflection;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Interfaces;
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Atlas : IEntity
{
	private string _name;
    private double _width;
    private double _height;
    private RgbColor _backgroundColor;

    public long Id { get; }
    public IList<Cartograph> Cartographs { get; }
    
    public string Name 
    {
        get => _name;
        set
        {
            AssertionHelper.EmptyOrWhitespaceCheck(value, nameof(value));

            _name = value;
        }
    }
    
    public double Width 
    {
        get => _width;
        set
        {
            AssertionHelper.PositiveCheck(value, nameof(value));
            
            _width = value;
        }
    }
    
    public double Height 
    {
        get => _height;
        set
        {
            AssertionHelper.PositiveCheck(value, nameof(value));

            _height = value;
        }
    }
    
    public RgbColor BackgroundColor 
    {
        get => _backgroundColor;
        set
        {
            AssertionHelper.NullCheck(value, nameof(value));

            _backgroundColor = value;
        }
    }
    
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

    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor, Cartograph cartograph)
        : this(id, name, width, height, backgroundColor)
    {
        AssertionHelper.NullCheck(cartograph, nameof(cartograph));
        
        Cartographs.Add(cartograph);
    }

    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor, IList<Cartograph> cartographs)
        : this(id, name, width, height, backgroundColor)
    {
        AssertionHelper.EmptyCheck(cartographs, nameof(cartographs));
        AssertionHelper.DuplicatesCheck(cartographs, nameof(cartographs));

        cartographs.ToList().ForEach(cartograph =>
        {
            // TODO: Devise a test to check for anonymous methods
            AssertionHelper.NullCheck(cartograph, nameof(cartographs));

            Cartographs.Add(cartograph);
        });
    }

    public Atlas(long id, Atlas existing)
    {
        AssertionHelper.NullCheck(existing, nameof(existing));
        AssertionHelper.PositiveCheck(id, nameof(id));
        
        Id = id;
        _name = existing.Name;
        _width = existing.Width;
        _height = existing.Height;
        _backgroundColor = existing.BackgroundColor;

        Cartographs = new List<Cartograph>(existing.Cartographs);
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

        return obj is Atlas atlas
               && atlas.Name.Equals(Name)
               && atlas.Width.Equals(Width)
               && atlas.Height.Equals(Height)
               && atlas.BackgroundColor.Equals(BackgroundColor)
               && atlas.Cartographs.All(Cartographs.Contains);
    }
}