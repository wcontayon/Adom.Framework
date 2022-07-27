using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.Collections
{
    internal class TrieNode
    {
        private char _value;
        private TrieNodeCollection _childrens;
        private TrieNode? _parent;
        private int _level;

        public TrieNode(char val, int level, TrieNode? parent)
        {
            _value = val;
            _level = level;
            _parent = parent;
            _childrens = new TrieNodeCollection();
        }

        public bool IsLeaf { get => _childrens == null || _childrens.Count == 0; }

        public char Value { get => _value; }

        public TrieNode? Parent { get => _parent; }

        public int Level { get => _level; }

        internal TrieNode? FindChar(char c)
        {
            Debug.Assert(_childrens != null);

            for (int i = 0; i< _childrens.Count; i++)
            {
                if (_childrens[i].Value == c)
                {
                    return _childrens[i];
                }
            }

            return null;
        }
    }

    internal class TrieNodeCollection : IEnumerable<TrieNode>
    {
        private List<TrieNode> _nodes;

        public TrieNodeCollection() => _nodes = new List<TrieNode>();

        public TrieNode this[int index] { get => _nodes[index]; set => _nodes[index] = value; }

        public int Count => _nodes.Count;

        public bool IsReadOnly => false;

        public void Add(TrieNode item) => _nodes.Add(item);

        public void Clear() => _nodes.Clear();

        public IEnumerator<TrieNode> GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();
    }

    public readonly ref struct SearchTrieResult
    {
        public readonly bool WordFound { get; }

        public readonly ReadOnlySpan<string> MatchedWords { get; }
    }

    public class Trie
    {
        private const char ROOTCHAR = '^';

        private readonly TrieNode? _root;

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

        public void InserWord(string word)
        {
            
        }

        private void Insert(ReadOnlySpan<char> word)
        {
            var currentNode = Prefix(word);
            if (currentNode != null)
            {
                for (int i = currentNode.Level; i < word.Length; i++)
                {
                    var node = new TrieNode(word[i], currentNode.Level + 1, currentNode);
                    // currentNode.
                }
            }
        }

        private TrieNode? PrefixWord(TrieNode? startNode, ReadOnlySpan<char> word)
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
    }
}
