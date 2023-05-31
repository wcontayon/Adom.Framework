using System;
using System.Linq;
using Bogus;
using Xunit;
using Adom.Framework;
using Moq;

namespace Adom.Framework.Collections
{
	public class TrieTest
	{
        [InlineData("axinduebc")]
        [InlineData("azzeruitoooyy")]
        [InlineData("azertyuiop")]
        [InlineData("qsdfghjklm")]
        [InlineData("wxcvbn")]
        [Theory]
        public void TrieInsertWordShouldCallPrefixWordMethod(string word)
        {
            var mockTrie = new Mock<Trie>();
            mockTrie.Object.InserWord(word);

            mockTrie.Verify(_ => _.Prefix(word), Times.Once);
            mockTrie.Verify(_ => _.PrefixWord(mockTrie.Object.Root, word), Times.Once);
        }

        [InlineData("azer", "azerty", "aert", "poiu")]
        [InlineData("qsdf", "qsdfg", "qsdfghj", "aze")]
        [Theory]
        public void TrieSearchSouldReturnNoResult(string word1, string word2, string word3, string search)
        {
            var mockTrie = new Mock<Trie>();
            mockTrie.Object.InserWord(word1);
            mockTrie.Object.InserWord(word2);
            mockTrie.Object.InserWord(word3);

            mockTrie.Verify(_ => _.Prefix(It.IsAny<string>()), Times.Exactly(3));
            mockTrie.Verify(_ => _.PrefixWord(mockTrie.Object.Root, It.IsAny<string>()), Times.Exactly(3));

            // Execute search
            var result = mockTrie.Object.FindWord(search);
            Assert.False(result.WordFound);
            Assert.Empty(result.MatchedWords.ToArray());
        }

        [InlineData("azer", "azerty", "aert", "aze")]
        [InlineData("qsdf", "qsdfg", "dfrvrv", "qsd")]
        [Theory]
        public void TrieSearchShouldReturnResult(string word1, string word2, string word3, string search)
        {
            // Arrange
            var mockTrie = new Mock<Trie>();
            mockTrie.Object.InserWord(word1);
            mockTrie.Object.InserWord(word2);
            mockTrie.Object.InserWord(word3);

            // Act
            var result = mockTrie.Object.FindWord(search);

            // Assert
            Assert.True(result.WordFound);
            Assert.NotEmpty(result.MatchedWords.ToArray());
        }
	}
}

