using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erumpere3D.Scripts
{
    public static class NodeExtentions
    {
        public static T FindNode<T>(this Node node)
        {
            if (node is T t) return t;
            foreach (var ch in node.GetChildren())
            {
                t = FindNode<T>(ch);
                if (t != null) return t;
            }
            return default;
        }


        public static IEnumerable<T> FindAllNode<T>(this Node node)
        {
            if (node is T t) yield return t;
            foreach (var ch in node.GetChildren())
            {
                if (ch is Node chNode)
                {
                    foreach (var tch in FindAllNode<T>(chNode))
                    {
                        yield return tch;
                    }
                }
            }
        }
    }
}
