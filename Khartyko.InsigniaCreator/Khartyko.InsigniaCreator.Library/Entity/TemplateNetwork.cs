﻿using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Library.Entity;

public class TemplateNetwork : NetworkBase
{
    public TemplateNetwork(IList<Node> nodes, IList<Link> links, IList<Cell> cells)
        : base(
            Validate(nodes, nameof(nodes)),
            Validate(links, nameof(links)),
            Validate(cells, nameof(cells))
        )
    {
    }
    
    /*
     * This note to Resharper is because I intentionally only want
     *  the TemplateNetwork to be created with an existing TemplateNetwork,
     *  and not the NetworkBase parent class
     * 
     * ReSharper disable once SuggestBaseTypeForParameterInConstructor
     */
    public TemplateNetwork(TemplateNetwork existing)
        : base(existing)
    {
    }

    public Node? GetNode(Vector2 position)
    {
        ObjectHelper.NullCheck(position, nameof(position));

        return Nodes.SingleOrDefault(node => position.Equals(node.Position));
    }

    private static IList<T> Validate<T>(IList<T> list, string descriptor)
    {
        if (!list.Any())
        {
            throw ExceptionHelper.GenerateArgumentException(
                typeof(TemplateNetwork),
                descriptor,
                $"'{descriptor}' cannot be empty"
            );
        }
        
        return list;
    }
}