using System.Collections.Generic;
using System.Linq;

namespace MiwDrzewo
{
    using System;
    
    public class Node
    {
        private static List<Node> nodes = new List<Node>();

        public static string GetCLIPSCode()
        {
            var result = string.Empty;
            foreach (var node in nodes)
            {
                result += node.GetRule() + Environment.NewLine + Environment.NewLine;
            }

            return result;
        }
        
        public Node()
        {
            nodes.Add(this);
        }
        
        public Node Parent { get; set; }
        public string Content { get; set; }
        public string Question { get; set; }
        public Node Yes { get; set; }
        public Node No { get; set; }
        private static int rules = 0;
        
        public string GetRule()
        {
            var rule = $"(defrule R{rules}" + Environment.NewLine;
            if (Parent == null)
            {
                rule += "\t?x <- (initial-fact)" + Environment.NewLine;
                rule += "\t=>" + Environment.NewLine;
            }
            else
            {
                rule += $"\t?x <- ({GetRuleString(Parent.Question)} " + (Parent.Yes == this ? "t" : "n")  + ")" + Environment.NewLine;
                rule += "\t=>" + Environment.NewLine;
            }
            
            rule += "(retract ?x)" + Environment.NewLine;
            rule += "(printout t crlf)" + Environment.NewLine;
            var lines = Content.Split('\n');
            if (lines != null)
            {
                rule = lines.Aggregate(rule,
                    (current, line) => current + ($"(printout t \"{line}\" crlf)" + Environment.NewLine));
            }

            if (Yes != null || No != null)
            {
                rule += $"(printout t \"{Question}\" crlf)" + Environment.NewLine;
                rule += $"(assert ({GetRuleString(Question)} (read))))";
            }
            else
            {
                rule += "(assert (koniec)))";
            }
            
            rules++;
            return rule;
        }

        private string GetRuleString(string rule)
        {
            var result = rule;
            if (rule.Contains('?'))
            {
                result = result.Remove('?');
            }
            if (rule.Contains(' '))
            {
                result = result.Replace(' ', '-');
            }

            return result.ToLower();
        }
    }
}