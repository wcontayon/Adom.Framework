using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Adom.Framework.Tests")]

namespace Adom.Framework.Collections
{
    internal class TrieNode
    {
        private char _value;
        private TrieNodeCollection _childrens;
        private TrieNode? _parent;
        private int _level;
        private string _word = string.Empty;

        public TrieNode(char val, int level, TrieNode? parent)
        {
            _value = val;
            _level = level;
            _parent = parent;
            _childrens = new TrieNodeCollection();
            // _word = wordPrefix;
        }

        public bool IsLeaf { get => _childrens == null || _childrens.Count == 0; }

        public char Value { get => _value; }

        public TrieNode? Parent { get => _parent; }

        public TrieNodeCollection Children { get => _childrens; }

        public int Level { get => _level; }

        public string Word { get => _word; }

        internal TrieNode? FindChar(char c)
        {
            Debug.Assert(_childrens != null);

            for (int i = 0; i < _childrens.Count; i++)
            {
                if (_childrens[i].Value == c)
                {
                    return _childrens[i];
                }
            }

            return null;
        }

        internal void DeleteChar(char c)
        {
            Debug.Assert(_childrens != null);
            for (var i = 0; i < _childrens.Count; i++)
            {
                if (_childrens[i].Value == c)
                {
                    _childrens.RemoveAt(i);
                }
            }
        }

    }

    internal class TrieNodeCollection : IEnumerable<TrieNode>
    {
        internal List<TrieNode> _nodes;

        public TrieNodeCollection() => _nodes = new List<TrieNode>();

        public TrieNode this[int index] { get => _nodes[index]; set => _nodes[index] = value; }

        public int Count => _nodes.Count;

        public bool IsReadOnly => false;

        public void Add(TrieNode item) => _nodes.Add(item);

        public void Clear() => _nodes.Clear();

        public void RemoveAt(int index) => _nodes.RemoveAt(index);

        public IEnumerator<TrieNode> GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
    }

    public readonly ref struct SearchTrieResult
    {
        public readonly bool WordFound { get; }

        public readonly ReadOnlySpan<string> MatchedWords { get; }

        public SearchTrieResult(bool found, string[] matches)
        {
            WordFound = found;
            MatchedWords = matches;
        }
    }

    public class Trie
    {
        private const char ROOTCHAR = '^';
        private const char ENDCHAR = '$';

        private readonly TrieNode? _root;
        internal TrieNode Root { get => _root; }

        public Trie()
        {
            _root = new TrieNode(ROOTCHAR, 0, null);
        }

        internal TrieNode? Prefix(string word)
        {
            var result = PrefixWord(_root, word.AsSpan());
            return result;
        }

        internal TrieNode? Prefix(ReadOnlySpan<char> word)
        {
            var result = PrefixWord(_root, word);
            return result;
        }

        public void InserWord(string word) => Insert(word.AsSpan());

        private void Insert(ReadOnlySpan<char> word)
        {
            var currentNode = Prefix(word);
            if (currentNode != null)
            {
                for (int i = currentNode.Level; i < word.Length; i++)
                {
                    var node = new TrieNode(word[i], currentNode.Level + 1, currentNode);
                    currentNode.Children.Add(node);
                    currentNode = node;
                }

                currentNode.Children.Add(new TrieNode(ENDCHAR, currentNode.Level + 1, currentNode));
            }
        }

        public SearchTrieResult FindWord(string word)
        {
            string prefixFound = string.Empty;
            var node = Prefix(word.AsSpan());

            if (node != null)
            {
                var matchedWords = new List<string>();

                // Add the word found
                matchedWords.Add(word);

                foreach (var child in node.Children)
                {
                    var matched = BuildPrefixWord(child);
                    if (matched == string.Empty)
                    {
                        matchedWords.Add(matched);
                    }
                }

                return new SearchTrieResult(true, matchedWords.ToArray());
            }
            else
            {
                return new SearchTrieResult(false, Array.Empty<string>());
            }
        }

        internal TrieNode? PrefixWord(TrieNode? startNode, ReadOnlySpan<char> word)
        {
            Debug.Assert(startNode != null);

            foreach (char c in word)
            {
                startNode = startNode!.FindChar(c);
                if (startNode == null)
                {
                    break;
                }
            }

            return startNode;
        }

        internal string BuildPrefixWord(TrieNode node)
        {
            string word = string.Empty;
            ValueStringBuilder builder = new ValueStringBuilder();
            builder.Append(node.Value);
            var i = 0;
            while (node.Children[i].Value != ENDCHAR)
            {
                builder.Append(node.Children[i].Value);
            }

            word = builder.ToString();
            return word;
        }
    }
}
