using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Tests
{
    [TestFixture]
    public class WalkInMatrix_Should
    {
        [TestCase(2)]
        [TestCase(100)]
        [TestCase(6)]
        [TestCase(55)]
        public void StartFromTheTopLeftCorner(int matrixSize)
        {
            // Walk in the matrix starts from the top left corner of the matrix.

            // Arrange
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);

            var writer = new Mock<IWritable>();

            // Act
            var walk = new WalkInMatrix(reader.Object, writer.Object);
            walk.Execute();

            // Assert
            Assert.AreEqual(1, walk.Matrix[0, 0]);
        }

        [TestCase(2)]
        [TestCase(100)]
        [TestCase(6)]
        [TestCase(55)]
        public void GoInDownRightDirectionToTheBottomRightCorner(int matrixSize)
        {
            // Goes in down-right direction.

            // Arrange
            // matrixSize is always the value at the bottom-right corner(matrix is square)
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);

            var writer = new Mock<IWritable>();

            var walk = new WalkInMatrix(reader.Object, writer.Object);

            // Act
            walk.Execute();

            // Assert
            Assert.AreEqual(matrixSize, walk.Matrix[matrixSize - 1, matrixSize - 1]);
        }

        [TestCase(2)]
        [TestCase(100)]
        [TestCase(6)]
        [TestCase(55)]
        public void ChangeDirectionClockwise_WhenNoContinuationIsAvailableAtCurrentDirection(int matrixSize)
        {
            // When no continuation is available at the current direction (either the matrix wall or non-empty cell is
            // reached), the direction is changed to the next possible clockwise.

            // Arrange
            // matrixSize is always the value at the bottom-right corner(matrix is square)
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);

            var writer = new Mock<IWritable>();

            var walk = new WalkInMatrix(reader.Object, writer.Object);

            // Act 
            walk.Execute();

            // Assert 
            // (matrixSize + 1) --> is always the value at the first possible cell clockwise
            // walk.Matrix[matrixSize - 1, matrixSize - 2] --> is always the coordinates of the first possible cell clockwise
            Assert.AreEqual(matrixSize + 1, walk.Matrix[matrixSize - 1, matrixSize - 2]);
        }

        [Test]
        public void RestartTheWalk_WhenNoEmptyCellIsAvailableAtAllDirections_ButThereAreStillEmptyCellsInMatrix()
        {
            // this test requires matrixSize >= 4, since there will be no empty cells after the first walk otherwise

            // When no empty cell is available at all directions, the walk is restarted from an empty cell at the
            // smallest possible row and as close as possible to the start of this row.

            // Arrange 
            int matrixSize = 6;
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);

            var writer = new Mock<IWritable>();

            var walk = new WalkInMatrix(reader.Object, writer.Object);

            // Act
            walk.Execute();

            // Assert
            Assert.AreEqual(31, walk.Matrix[2, 1]);
        }

        [TestCase(2)]
        [TestCase(100)]
        [TestCase(6)]
        public void PrintTheMatrix_WhenNoEmptyCellIsLeft(int matrixSize)
        {
            // When no empty cell is left in the matrix, the walk is finished.

            // Arrange
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);

            var writer = new Mock<IWritable>();

            var walk = new WalkInMatrix(reader.Object, writer.Object);

            // Act
            walk.Execute();
            
            // Assert
            writer.Verify(x => x.Write(It.IsAny<string>()));
        }

        [Test]
        public void ThrowArgumentOutOfRangeException_WhenMatrixSizePassedIsLessThan1()
        {
            // Arrange
            int matrixSize = -1;
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);
            var writer = new Mock<IWritable>();
            
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new WalkInMatrix(reader.Object, writer.Object));
        }

        [Test]
        public void ThrowArgumentOutOfRangeException_WhenMatrixSizePassedIsGreaterThan100()
        {
            // Arrange
            int matrixSize = 101;
            var reader = new Mock<IReadable>();
            reader.Setup(x => x.ReadSize()).Returns(matrixSize);
            var writer = new Mock<IWritable>();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new WalkInMatrix(reader.Object, writer.Object));
        }
    }
}
