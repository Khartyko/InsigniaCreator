using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Interfaces;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS0659

namespace Khartyko.InsigniaCreator.Library.Entity;

public class Atlas : IEntity
{
    /*
     * TODO: Come up with something more sensible than this number.
     *  I know someone wouldn't reasonably have a 32k monitor,
     *  but I can't completely ignore it. But checking a max boundary of
     *  double.MaxValue would be very inefficient and unlikely.
     *
     * TODO: Add this to the configuration so it can be easily changed later
     */
    private static readonly double s_maxBoundaryLength = 32768;
    
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
            StringHelper.EmptyOrWhitespaceCheck(value, nameof(value));

            _name = value;
        }
    }
    
    public double Width 
    {
        get => _width;
        set
        {
            MathHelper.RangeCheck(value, 1, s_maxBoundaryLength, nameof(value));
            
            _width = value;
        }
    }
    
    public double Height 
    {
        get => _height;
        set
        {
            MathHelper.RangeCheck(value, 1, s_maxBoundaryLength, nameof(value));

            _height = value;
        }
    }
    
    public RgbColor BackgroundColor 
    {
        get => _backgroundColor;
        set
        {
            ObjectHelper.NullCheck(value, nameof(value));

            _backgroundColor = value;
        }
    }
    
    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor)
    {
        if (id < 0)
        {
            throw new ArgumentException($"Atlas::Atlas(>id<, name, width, height, backgroundColor); 'id' cannot be less than 0 (got '{id}')", nameof(id));
        }
        
        StringHelper.EmptyOrWhitespaceCheck(name, nameof(name));
        
        MathHelper.RangeCheck(width, 0, s_maxBoundaryLength, nameof(width));
        MathHelper.RangeCheck(height, 0, s_maxBoundaryLength, nameof(height));
        
        ObjectHelper.NullCheck(backgroundColor, nameof(backgroundColor));
        
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
        ObjectHelper.NullCheck(cartograph, nameof(cartograph));
        
        Cartographs.Add(cartograph);
    }

    public Atlas(long id, string name, double width, double height, RgbColor backgroundColor, IList<Cartograph> cartographs)
        : this(id, name, width, height, backgroundColor)
    {
        ObjectHelper.NullCheck(cartographs, nameof(cartographs));

        if (!cartographs.Any())
        {
            throw new ArgumentException(
        $"'cartographs' cannot be empty",
                nameof(cartographs)
            );
        }

        for (var i = 0; i < cartographs.Count; i++)
        {
            Cartograph cartograph = cartographs[i];
            
            ObjectHelper.NullCheck(cartograph, $"{nameof(cartographs)}[{i}]");
        }
        
        List<Cartograph> duplicates = FindDuplicates(cartographs);
        
        if (duplicates.Any())
        {
            throw ExceptionHelper.GenerateArgumentException(
                GetType(),
                nameof(cartographs),
                $"'cartographs' cannot have duplicate ids; (got '{string.Join(", ", duplicates)}')"
            );
        }

        cartographs.ToList().ForEach(Cartographs.Add);
    }

    public Atlas(long id, Atlas existing)
    {
        ObjectHelper.NullCheck(existing, nameof(existing));
        
        if (id < 0)
        {
            throw new ArgumentException(
                $"Atlas::Atlas(id, >existing<); 'id' cannot be less than 0 (got '{id}')", nameof(id));
        }

        Id = id;
        _name = existing.Name;
        _width = existing.Width;
        _height = existing.Height;
        _backgroundColor = existing.BackgroundColor;

        Cartographs = new List<Cartograph>(existing.Cartographs);
    }

    private static List<Cartograph> FindDuplicates(IList<Cartograph> cartographs)
    {
        var ids = cartographs.Select(cartograph => cartograph.Id).ToList();

        return cartographs.Where(cartograph => ids.Count(id => id == cartograph.Id) > 1)
                        .ToList();
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