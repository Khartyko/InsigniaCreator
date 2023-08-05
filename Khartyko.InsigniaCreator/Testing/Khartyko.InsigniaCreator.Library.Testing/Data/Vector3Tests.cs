using Khartyko.InsigniaCreator.Library.Data;

#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Data;

public class Vector3Tests
{
        [Theory]
        [InlineData(1.0)]
        [InlineData(-1.0)]
        [InlineData(-3.2)]
        public void Vector3_Create_FromSingleValue_Succeeds(double value)
        {
            var vec = new Vector3(value);

            Assert.Equal(value, vec.X);
            Assert.Equal(value, vec.Y);
            Assert.Equal(1, vec.Z);
        }

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void Vector3_Create_FromSingleValue_Fails(double value)
        {
            Assert.Throws<ArgumentException>(() => new Vector3(value));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(-2, 2)]
        public void Vector3_Create_FromXY_Succeeds(double x, double y)
        {
            var vec = new Vector3(x, y);

            Assert.Equal(x, vec.X);
            Assert.Equal(y, vec.Y);
        }

        [Theory]
        [InlineData(double.NaN, 1.0)]
        [InlineData(double.PositiveInfinity, 1.0)]
        [InlineData(double.NegativeInfinity, 1.0)]
        [InlineData(1.0, double.NaN)]
        [InlineData(1.0, double.PositiveInfinity)]
        [InlineData(1.0, double.NegativeInfinity)]
        public void Vector3_Create_FromXY_Fails(double x, double y)
        {
            Assert.Throws<ArgumentException>(() => new Vector3(x, y));
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0)]
        [InlineData(-2.0, 1.0, -3.0)]
        public void Vector3_Create_FromXYZ_Succeeds(double x, double y, double z)
        {
            var vec = new Vector3(x, y, z);

            Assert.Equal(x, vec.X);
            Assert.Equal(y, vec.Y);
            Assert.Equal(z, vec.Z);
        }

        [Theory]
        [InlineData(double.NaN, 0, 0)]
        [InlineData(double.PositiveInfinity, 0, 0)]
        [InlineData(double.NegativeInfinity, 0, 0)]
        [InlineData(0, double.NaN, 0)]
        [InlineData(0, double.PositiveInfinity, 0)]
        [InlineData(0, double.NegativeInfinity, 0)]
        [InlineData(0, 0, double.NaN)]
        [InlineData(0, 0, double.PositiveInfinity)]
        [InlineData(0, 0, double.NegativeInfinity)]
        public void Vector3_Create_FromXYZ_Fails(double x, double y, double z)
        {
            Assert.Throws<ArgumentException>(() => new Vector3(x, y, z));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(-2, 2)]
        public void Vector3_Create_FromExistingVector2_Succeeds(double x, double y)
        {
            var initial = new Vector2(x, y);
            var duplicate = new Vector3(initial);

            Assert.Equal(x, duplicate.X);
            Assert.Equal(y, duplicate.Y);
        }

        [Fact]
        public void Vector3_Create_FromExistingVector2_Fails()
        {
            Vector2 nullVector = null;

            Assert.Throws<ArgumentNullException>(() => new Vector3(nullVector));
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0)]
        [InlineData(-2.0, 1.0, -3.0)]
        public void Vector3_Create_FromExistingVector3_Succeeds(double x, double y, double z)
        {
            var initial = new Vector3(x, y, z);
            var duplicate = new Vector3(initial);

            Assert.Equal(x, duplicate.X);
            Assert.Equal(y, duplicate.Y);
            Assert.Equal(z, duplicate.Z);
        }

        [Fact]
        public void Vector3_Create_FromExistingVector3_Fails()
        {
            Vector3 nullVector = null;
            
            Assert.Throws<ArgumentNullException>(() => new Vector3(nullVector));
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, 0.0)]
        [InlineData(-2.0, 1.0, -3.0, 2.5)]
        public void Vector3_X_Succeeds(double x, double y, double z, double xUpdate)
        {
            var vec = new Vector3(x, y, z);

            Assert.Equal(x, vec.X);
            vec.X = xUpdate;
            Assert.Equal(xUpdate, vec.X);
            Assert.NotEqual(x, vec.X);
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, double.NaN)]
        [InlineData(1.0, 1.0, 1.0, double.PositiveInfinity)]
        [InlineData(1.0, 1.0, 1.0, double.NegativeInfinity)]
        public void Vector3_X_Fails(double x, double y, double z, double xUpdate)
        {
            var vec = new Vector3(x, y, z);

            Assert.Throws<ArgumentException>(() => vec.X = xUpdate);
            Assert.Equal(x, vec.X);
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, 0.0)]
        [InlineData(-2.0, 1.0, -3.0, 2.5)]
        public void Vector3_Y_Succeeds(double x, double y, double z, double yUpdate)
        {
            var vec = new Vector3(x, y, z);
            Assert.Equal(y, vec.Y);
            vec.Y = yUpdate;
            Assert.Equal(yUpdate, vec.Y);
            Assert.NotEqual(y, vec.Y);
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, double.NaN)]
        [InlineData(1.0, 1.0, 1.0, double.PositiveInfinity)]
        [InlineData(1.0, 1.0, 1.0, double.NegativeInfinity)]
        public void Vector3_Y_Fails(double x, double y, double z, double yUpdate)
        {
            var vec = new Vector3(x, y, z);

            Assert.Throws<ArgumentException>(() => vec.Y = yUpdate);
            Assert.Equal(x, vec.X);
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, 0.0)]
        [InlineData(-2.0, 1.0, -3.0, 2.5)]
        public void Vector3_Z_Succeeds(double x, double y, double z, double zUpdate)
        {
            var vec = new Vector3(x, y, z);
            Assert.Equal(z, vec.Z);
            vec.Z = zUpdate;
            Assert.Equal(zUpdate, vec.Z);
            Assert.NotEqual(z, vec.Z);
        }

        [Theory]
        [InlineData(1.0, 1.0, 1.0, double.NaN)]
        [InlineData(1.0, 1.0, 1.0, double.PositiveInfinity)]
        [InlineData(1.0, 1.0, 1.0, double.NegativeInfinity)]
        public void Vector3_Z_Fails(double x, double y, double z, double zUpdate)
        {
            var vec = new Vector3(x, y, z);

            Assert.Throws<ArgumentException>(() => vec.Z = zUpdate);
            Assert.Equal(x, vec.X);
        }

        [Theory]
        [InlineData(1, 1, 1, 1.732)]
        [InlineData(5, 5, 5, 8.660)]
        public void Vector3_Length_Succeeds(double x, double y, double z, double expectedLength)
        {
            var vec = new Vector3(x, y, z);

            Assert.Equal(expectedLength, vec.Length);
        }

        [Theory]
        [InlineData(1, 1, 1, 0, 2)]
        [InlineData(2, -1, 3, 1, 5)]
        public void Vector3_Index_Succeeds(double x, double y, double z, int index, double valueUpdate)
        {
            var vec = new Vector3(x, y, z);

            vec[index] = valueUpdate;
            Assert.Equal(valueUpdate, vec[index]);
        }

        [Theory]
        [InlineData(1, 1, 1, -1)]
        [InlineData(2, -1, 3, 3)]
        public void Vector3_Index_Fails_BadIndex(double x, double y, double z, int index)
        {
            var vec = new Vector3(x, y, z);

            Assert.Throws<ArgumentOutOfRangeException>(() => vec[index]);
        }

        [Theory]
        [InlineData(1, 1, 1, 0, double.NaN)]
        [InlineData(2, -1, 3, 1, double.PositiveInfinity)]
        [InlineData(2, -1, 3, 1, double.NegativeInfinity)]
        public void Vector3_Index_Fails_BadValueUpdate(double x, double y, double z, int index, double valueUpdate)
        {
            var vec = new Vector3(x, y, z);

            Assert.Throws<ArgumentException>(() => vec[index] = valueUpdate);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 2, 2)]
        [InlineData(1, 2, 3, 2, 1, 4, 4)]
        public void Vector3_AdditionOperator_V2V3_Succeeds(double x0, double y0, double x1, double y1, double z1, double expectedX, double expectedY)
        {
            var vec2 = new Vector2(x0, y0);
            var vec3 = new Vector3(x1, y1, z1);

            var actual = vec2 + vec3;

            Assert.Equal(expectedX, actual.X);
            Assert.Equal(expectedY, actual.Y);
        }

        [Fact]
        public void Vector3_AdditionOperator_V2V3_Fails()
        {
            Vector2 nullVec2 = null;
            Vector3 nullVec3 = null;
            var vec2 = new Vector2(1);
            var vec3 = new Vector3(1);

            Assert.Throws<ArgumentNullException>(() => vec2 + nullVec3);
            Assert.Throws<ArgumentNullException>(() => nullVec2 + vec3);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 2, 2)]
        [InlineData(-1, 1, -1, 1, -1, 0, 0)]
        public void Vector3_AdditionOperator_V3V2_Succeeds(double x0, double y0, double z0, double x1, double y1, double expectedX, double expectedY)
        {
            var vec3 = new Vector3(x0, y0, z0);
            var vec2 = new Vector2(x1, y1);

            var actual = vec3 + vec2;

            Assert.Equal(expectedX, actual.X);
            Assert.Equal(expectedY, actual.Y);
            Assert.Equal(z0, actual.Z);
        }

        [Fact]
        public void Vector3_AdditionOperator_V3V2_Fails()
        {
            Vector2 nullVec2 = null;
            Vector3 nullVec3 = null;
            var vec2 = new Vector2(1);
            var vec3 = new Vector3(1);

            Assert.Throws<ArgumentNullException>(() => vec3 + nullVec2);
            Assert.Throws<ArgumentNullException>(() => nullVec3 + vec2);
        }

        [Theory]
        [InlineData(1, 1, 1, -1, -1, -1, 0, 0, 0)]
        [InlineData(1, 1, 1, -2, -2, -2, -1, -1, -1)]
        public void Vector3_AdditionOperator_V3V3_Succeeds(double x0, double y0, double z0, double x1, double y1, double z1, double expectedX, double expectedY, double expectedZ)
        {
            var vec0 = new Vector3(x0, y0, z0);
            var vec1 = new Vector3(x1, y1, z1);

            var actual = vec0 + vec1;

            Assert.Equal(expectedX, actual.X);
            Assert.Equal(expectedY, actual.Y);
            Assert.Equal(expectedZ, actual.Z);
        }

        [Fact]
        public void Vector3_AdditionOperator_V3V3_Fails()
        {
            Vector3 nullVector = null;
            var vec = new Vector3(1);

            Assert.Throws<ArgumentNullException>(() => vec + nullVector);
            Assert.Throws<ArgumentNullException>(() => nullVector + vec);
        }
        
        [Theory]
        [InlineData(1, 1, 1, 1, 1, 0, 0)]
        [InlineData(1, 2, 3, 2, 1, -2, 0)]
        public void Vector3_SubtractionOperator_V2V3_Succeeds(double x0, double y0, double x1, double y1, double z1, double expectedX, double expectedY)
        {
            var vec2 = new Vector2(x0, y0);
            var vec3 = new Vector3(x1, y1, z1);

            var actual = vec2 - vec3;

            Assert.Equal(expectedX, actual.X);
            Assert.Equal(expectedY, actual.Y);
        }

        [Fact]
        public void Vector3_SubtractionOperator_V2V3_Fails()
        {
            Vector2 nullVec2 = null;
            Vector3 nullVec3 = null;
            var vec2 = new Vector2(1);
            var vec3 = new Vector3(1);

            Assert.Throws<ArgumentNullException>(() => vec2 - nullVec3);
            Assert.Throws<ArgumentNullException>(() => nullVec2 - vec3);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 0, 0)]
        [InlineData(-1, 1, -1, 1, -1, -2, 2)]
        public void Vector3_SubtractionOperator_V3V2_Succeeds(double x0, double y0, double z0, double x1, double y1, double expectedX, double expectedY)
        {
            var vec3 = new Vector3(x0, y0, z0);
            var vec2 = new Vector2(x1, y1);

            var actual = vec3 - vec2;

            Assert.Equal(expectedX, actual.X);
            Assert.Equal(expectedY, actual.Y);
            Assert.Equal(z0, actual.Z);
        }

        [Fact]
        public void Vector3_SubtractionOperator_V3V2_Fails()
        {
            Vector2 nullVec2 = null;
            Vector3 nullVec3 = null;
            var vec2 = new Vector2(1);
            var vec3 = new Vector3(1);

            Assert.Throws<ArgumentNullException>(() => vec3 - nullVec2);
            Assert.Throws<ArgumentNullException>(() => nullVec3 - vec2);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 1, 0, 0, 0)]
        [InlineData(2, 2, 2, -1, -1, -1, 3, 3, 3)]
        public void Vector3_SubtractionOperator_V3V3_Succeeds(double x0, double y0, double z0, double x1, double y1, double z1, double expectedX, double expectedY, double expectedZ)
        {
            var vec0 = new Vector3(x0, y0, z0);
            var vec1 = new Vector3(x1, y1, z1);

            var actual = vec0 - vec1;

            Assert.Equal(expectedX, actual.X);
            Assert.Equal(expectedY, actual.Y);
            Assert.Equal(expectedZ, actual.Z);
        }

        [Fact]
        public void Vector3_SubtractionOperator_V3V3_Fails()
        {
            Vector3 nullVector = null;
            var vec = new Vector3(1);

            Assert.Throws<ArgumentNullException>(() => vec - nullVector);
            Assert.Throws<ArgumentNullException>(() => nullVector - vec);
        }
        
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, -1, 5)]
        public void Vector3_Equals_Succeeds(double x0, double y0, double z0)
        {
            var vec0 = new Vector3(x0, y0, z0);
            var vec1 = new Vector3(x0, y0, z0);

            Assert.True(vec0 == vec1);
        }

        [Theory]
        [InlineData(1, 2, 3, 3, 2, 1)]
        [InlineData(-1, -2, -3, 1, 2, 3)]
        public void Vector3_NotEquals_Succeeds(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            var vec0 = new Vector3(x0, y0, z0);
            var vec1 = new Vector3(x1, y1, z1);

            Assert.True(vec0 != vec1);
        }
}