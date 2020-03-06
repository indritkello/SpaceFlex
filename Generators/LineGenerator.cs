using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceFlex.Generators
{
    public class LineGenerator
    {
        public static VertexPositionColor[]  CreateRectangleLine(int topLeftX, int topLeftY, int width, int height, Color solidColor)
        {
            var verts = new VertexPositionColor[6];
            //First triangle part
            verts[0] = new VertexPositionColor(new Vector3(topLeftX, topLeftY, 0), solidColor);
            verts[1] = new VertexPositionColor(new Vector3(topLeftX + width, topLeftY, 0), solidColor);
            verts[2] = new VertexPositionColor(new Vector3(topLeftX + width, topLeftY - height, 0), solidColor);

            //Second part
            verts[3] = new VertexPositionColor(new Vector3(topLeftX + width, topLeftY - height, 0), solidColor);
            verts[4] = new VertexPositionColor(new Vector3(topLeftX, topLeftY - height, 0), solidColor);
            verts[5] = new VertexPositionColor(new Vector3(topLeftX, topLeftY, 0), solidColor);
            return verts;
        }
    }
}
