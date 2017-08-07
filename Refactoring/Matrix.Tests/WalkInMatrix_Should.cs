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
        [Test]
        public void StartFromTheTopLeftCorner()
        {
            // Walk in the matrix is walk that starts form the top left corner of the matrix 
            // Arrange
            var walk = new Mock<WalkInMatrix>();

            // Act


            // Assert

        }

        [Test]
        public void GoInDownRightDirectionToTheBottomRightCorner()
        {
            // Goes in down-right direction
        }

        [Test]
        public void ChangeDirection_WhenNoContinuationIsAvailableAtCurrentDirection()
        {
            // When no continuation is available at the current direction (either the matrix wall or non-empty cell is
            // reached), the direction is changed to the next possible clockwise.
        }

        [Test]
        public void RestartTheWalk_WhenNoEmptyCellIsAvailableAtAllDirections()
        {
            // When no empty cell is available at all directions, the walk is restarted from an empty cell at the
            // smallest possible row and as close as possible to the start of this row.
        }

        [Test]
        public void PrintTheMatrix_WhenNoEmptyCellIsLeft()
        {
            // When no empty cell is left in the matrix, the walk is finished.
        }
    }
}
