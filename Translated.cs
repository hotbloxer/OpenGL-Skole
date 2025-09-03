using Microsoft.VisualBasic.Devices;
using OpenTK.Compute.OpenCL;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OpenGL
{

    public class ProgramTest
    {















        function ValidateShaderProgram(p)
        {
            gl.validateProgram(p);

            // Check if validation was successful
            if (!gl.getProgramParameter(p, gl.VALIDATE_STATUS))
            {
                // Write error to console
                console.error(gl.getProgramInfoLog(p));
                alert('Errors found validating shader program');
                return false;
            }
            return true;
        }






        // Global variable
        var vertices = [];
        var gl = document.getElementById('gl')
                        .getContext('webgl') ||
                // Support Internet Explorer, Edge, Safari
                document.getElementById('gl')
                        .getContext('experimental-webgl');


        function AddVertex(x, y, z, r, g, b, u, v, nx, ny, nz)
        {
            // get index denne vertecy fylder
            const index = vertices.length;

            // udvid array med længden af en vertecy
            vertices.length += 11;

            // sæt værdien af vertecies
            vertices[index + 0] = x;
            vertices[index + 1] = y;
            vertices[index + 2] = z;
            vertices[index + 3] = r;
            vertices[index + 4] = g;
            vertices[index + 5] = b;
            vertices[index + 6] = u;
            vertices[index + 7] = v;
            vertices[index + 8] = nx;
            vertices[index + 9] = ny;
            vertices[index + 10] = nz;
        }



        function CreateTriangle(width, height)
        {
            vertices.length = 0;
            const w = width * 0.5;
            const h = height * 0.5;
            AddTriangle(0.0, h, 0.0, 1.0, 0.0, 0.0, 0.5, 1.0, 0.0, 0.0, -1.0,
                             -w, -h, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0,
                              w, -h, 0.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, -1.0);
        }

        function AddTriangle(
            // input kanter i trekant
            x1, y1, z1, r1, g1, b1, u1, v1, nx1, ny1, nz1,
            x2, y2, z2, r2, g2, b2, u2, v2, nx2, ny2, nz2,
            x3, y3, z3, r3, g3, b3, u3, v3, nx3, ny3, nz3)

        {
            // tilføj vertecies til functionen
            AddVertex(x1, y1, z1, r1, g1, b1, u1, v1, nx1, ny1, nz1);
            AddVertex(x2, y2, z2, r2, g2, b2, u2, v2, nx2, ny2, nz2);
            AddVertex(x3, y3, z3, r3, g3, b3, u3, v3, nx3, ny3, nz3);
        }



        function CreateQuad(width, height)
        {
            vertices.length = 0;
            const w = width * 0.5;
            const h = height * 0.5;
            AddQuad(-w, h, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, -1.0,
                    -w, -h, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0,
                     w, -h, 0.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, -1.0,
                     w, h, 0.0, 1.0, 1.0, 0.0, 1.0, 1.0, 0.0, 0.0, -1.0);
        }


        function AddQuad(
            x1, y1, z1, r1, g1, b1, u1, v1, nx1, ny1, nz1,
            x2, y2, z2, r2, g2, b2, u2, v2, nx2, ny2, nz2,
            x3, y3, z3, r3, g3, b3, u3, v3, nx3, ny3, nz3,
            x4, y4, z4, r4, g4, b4, u4, v4, nx4, ny4, ny4)
        {
            AddTriangle(
                x1, y1, z1, r1, g1, b1, u1, v1, nx1, ny1, nz1,
                x2, y2, z2, r2, g2, b2, u2, v2, nx2, ny2, nz2,
                x3, y3, z3, r3, g3, b3, u3, v3, nx3, ny3, nz3);

            AddTriangle(
                x3, y3, z3, r3, g3, b3, u3, v3, nx3, ny3, nz3,
                x4, y4, z4, r4, g4, b4, u4, v4, nx4, ny4, ny4,
                x1, y1, z1, r1, g1, b1, u1, v1, nx1, ny1, nz1);
        }


        function CreateBox(width, height, depth)
        {
            vertices.length = 0;
            const w = width * 0.5;
            const h = height * 0.5;
            const d = depth * 0.5;

            //front
            AddQuad(-w, h, d, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 1.0,
                    -w, -h, d, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0,
                     w, -h, d, 1.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0,
                     w, h, d, 1.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 1.0);


            //back
            AddQuad(-w, h, -d, 0.0, 0.0, 1.0, 1.0, 1.0, 0.0, 0.0, -1.0,
                     w, h, -d, 0.0, 0.0, 1.0, 0.0, 1.0, 0.0, 0.0, -1.0,
                     w, -h, -d, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0,
                    -w, -h, -d, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, -1.0);

            //left
            AddQuad(w, h, d, 0.0, 1.0, 0.0, 0.0, 1.0, 1.0, 0.0, 0.0,
                    w, -h, d, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0,
                    w, -h, -d, 0.0, 1.0, 0.0, 1.0, 0.0, 1.0, 0.0, 0.0,
                    w, h, -d, 0.0, 1.0, 0.0, 1.0, 1.0, 1.0, 0.0, 0.0);


            //right
            AddQuad(-w, h, d, 1.0, 0.0, 1.0, 1.0, 1.0, -1.0, 0.0, 0.0,
                     -w, h, -d, 1.0, 0.0, 1.0, 0.0, 1.0, -1.0, 0.0, 0.0,
                     -w, -h, -d, 1.0, 0.0, 1.0, 0.0, 0.0, -1.0, 0.0, 0.0,
                     -w, -h, d, 1.0, 0.0, 1.0, 0.0, 1.0, -1.0, 0.0, 0.0);

            //top
            AddQuad(w, -h, d, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0, 0.0,
                    -w, -h, d, 0.0, 1.0, 1.0, 0.0, 1.0, 0.0, -1.0, 0.0,
                    -w, -h, -d, 0.0, 1.0, 1.0, 0.0, 0.0, 0.0, -1.0, 0.0,
                     w, -h, -d, 0.0, 1.0, 1.0, 1.0, 0.0, 0.0, -1.0, 0.0);
            //borrom
            AddQuad(w, h, d, 1.0, 1.0, 1.0, 0.0, 1.0, 0.0, 1.0, 0.0,
                     w, h, -d, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 1.0, 0.0,
                    -w, h, -d, 1.0, 1.0, 1.0, 1.0, 0.0, 0.0, 1.0, 0.0,
                    -w, h, d, 1.0, 1.0, 1.0, 1.0, 1.0, 0.0, 1.0, 0.0);
        }


        function CreateSubdividedBox(subdiv, width, height, depth)
        {

            // if positive direction is chosen, the locked plane will be in the positive side
            // fx top, right or back
            // back is a special case though
            vertices.length = 0;
            var white = false;
            var wc = 1.0;

            var w = width / subdiv;
            var h = height / subdiv;
            var d = depth / subdiv;

            var startPointX = width * 0.5 * -1;
            var startPointY = height * 0.5 * -1;
            var startPointD = depth * 0.5 * -1;

            for (let y = 0; y < subdiv; y++)
            {
                for (let x = 0; x < subdiv; x++)
                {
                    // Front
                    let c = (x + y) % 2 == 0 ? 1.0 : 0.0;
                    AddQuad(startPointX + w * x, startPointY + h * y, -startPointD, c, c, c, x / subdiv, y / subdiv, 0.0, 0.0, 1.0,    // 1
                            startPointX + w * (x + 1), startPointY + h * y, -startPointD, c, c, c, (x + 1) / subdiv, y / subdiv, 0.0, 0.0, 1.0,     // 4
                            startPointX + w * (x + 1), startPointY + h * (y + 1), -startPointD, c, c, c, (x + 1) / subdiv, (y + 1) / subdiv, 0.0, 0.0, 1.0, // 3
                            startPointX + w * x, startPointY + h * (y + 1), -startPointD, c, c, c, x / subdiv, (y + 1) / subdiv, 0.0, 0.0, 1.0  // 2
                   );
                    // Back
                    AddQuad(startPointX + w * x, startPointY + h * y, startPointD, c, c, c, -x / subdiv, y / subdiv, 0.0, 0.0, -1.0,       // 1
                            startPointX + w * x, startPointY + h * (y + 1), startPointD, c, c, c, -x / subdiv, (y + 1) / subdiv, 0.0, 0.0, -1.0,   // 4
                            startPointX + w * (x + 1), startPointY + h * (y + 1), startPointD, c, c, c, -(x + 1) / subdiv, (y + 1) / subdiv, 0.0, 0.0, -1.0,   // 3
                            startPointX + w * (x + 1), startPointY + h * y, startPointD, c, c, c, -(x + 1) / subdiv, y / subdiv, 0.0, 0.0, -1.0        // 2
                   );
                }
            }

            for (let y = 0; y < subdiv; y++)
            {
                for (let z = 0; z < subdiv; z++)
                {
                    // Left
                    let c = (z + y) % 2 == 0 ? 1.0 : 0.0;
                    AddQuad(startPointX, startPointY + h * y, startPointD + d * z, c, c, c, z / subdiv, y / subdiv, -1.0, 0.0, 0.0, // 1
                            startPointX, startPointY + h * y, startPointD + d * (z + 1), c, c, c, (z + 1) / subdiv, y / subdiv, -1.0, 0.0, 0.0, //4
                            startPointX, startPointY + h * (y + 1), startPointD + d * (z + 1), c, c, c, (z + 1) / subdiv, (y + 1) / subdiv, -1.0, 0.0, 0.0, // 3
                            startPointX, startPointY + h * (y + 1), startPointD + d * z, c, c, c, z / subdiv, (y + 1) / subdiv, -1.0, 0.0, 0.0 // 2
                   );

                    // Right
                    AddQuad(-startPointX, startPointY + h * y, startPointD + d * z, c, c, c, -z / subdiv, y / subdiv, 1.0, 0.0, 0.0, // 1
                             -startPointX, startPointY + h * (y + 1), startPointD + d * z, c, c, c, -z / subdiv, (y + 1) / subdiv, 1.0, 0.0, 0.0, // 2
                             -startPointX, startPointY + h * (y + 1), startPointD + d * (z + 1), c, c, c, -(z + 1) / subdiv, (y + 1) / subdiv, 1.0, 0.0, 0.0, // 3
                             -startPointX, startPointY + h * y, startPointD + d * (z + 1), c, c, c, -(z + 1) / subdiv, y / subdiv, 1.0, 0.0, 0.0  //4
                   );
                }
            }


            for (let y = 0; y < subdiv; y++)
            {
                for (let x = 0; x < subdiv; x++)
                {
                    // bottom
                    let c = (x + y) % 2 == 0 ? 1.0 : 0.0;
                    AddQuad(startPointX + w * x, startPointY, startPointD + h * y, c, c, c, x / subdiv, y / subdiv, 0.0, -1.0, 0.0, // 1
                            startPointX + w * (x + 1), startPointY, startPointD + h * y, c, c, c, (x + 1) / subdiv, y / subdiv, 0.0, -1.0, 0.0, //4
                            startPointX + w * (x + 1), startPointY, startPointD + h * (y + 1), c, c, c, (x + 1) / subdiv, (y + 1) / subdiv, 0.0, -1.0, 0.0, // 3
                            startPointX + w * x, startPointY, startPointD + h * (y + 1), c, c, c, x / subdiv, (y + 1) / subdiv, 0.0, -1.0, 0.0 // 2
                    );
                    // top
                    AddQuad(startPointX + w * x, -startPointY, startPointD + h * y, c, c, c, -x / subdiv, y / subdiv, 0.0, 1.0, 0.0, // 1
                            startPointX + w * x, -startPointY, startPointD + h * (y + 1), c, c, c, -x / subdiv, (y + 1) / subdiv, 0.0, 1.0, 0.0, // 2
                            startPointX + w * (x + 1), -startPointY, startPointD + h * (y + 1), c, c, c, -(x + 1) / subdiv, (y + 1) / subdiv, 0.0, 1.0, 0.0, // 3
                            startPointX + w * (x + 1), -startPointY, startPointD + h * y, c, c, c, -(x + 1) / subdiv, y / subdiv, 0.0, 1.0, 0.0//4
                    );
                }
            }
        }



        function CreateCylinder(radius, height, subdiv)
        {
            vertices.length = 0;
            const h = (height * 0.5) / 2;
            radius = radius / 2;


            var radian = Math.PI / 180;
            for (let i = 0; i < subdiv; i++)
            {

                let angleOfSub = 360 / subdiv
    

        let cornerLeftX = radius * Math.cos(radian * angleOfSub * i);
                let cornerLeftY = radius * Math.sin(radian * angleOfSub * i);
                let cornerRightX = radius * Math.cos(radian * angleOfSub * (i + 1))
                let cornerRightY = radius * Math.sin(radian * angleOfSub * (i + 1))
    
        // top part
        AddVertex(cornerRightX, h, cornerRightY, 1.0, 1.0, 1.0, cornerRightX, cornerRightY, 0.0, 0.0, -1.0);
                AddVertex(cornerLeftX, h, cornerLeftY, 1.0, 1.0, 1.0, cornerLeftX, cornerLeftY, 0.0, 0.0, -1.0);
                AddVertex(0.0, h, 0.0, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0);

                let normalDirx = Math.cos(radian * (angleOfSub * i - (angleOfSub * 0.5)));
                let normalDiry = Math.sin(radian * (angleOfSub * i - (angleOfSub * 0.5)));






                AddVertex(cornerLeftX, h, cornerLeftY, 1.0, 1.0, 1.0, 1.0, 1.0, 0.0, 0.0, -1.0);
                AddVertex(cornerRightX, h, cornerRightY, 1.0, 1.0, 0.0, 0.0, 1.0, 0.0, 0.0, -1.0);
                AddVertex(cornerRightX, -h, cornerRightY, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0);
                AddVertex(cornerLeftX, h, cornerLeftY, 1.0, 1.0, 1.0, 1.0, 1.0, 0.0, 0.0, -1.0);
                AddVertex(cornerRightX, -h, cornerRightY, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0);
                AddVertex(cornerLeftX, -h, cornerLeftY, 1.0, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, -1.0);


                // botom part

                AddVertex(0.0, -h, 0.0, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0);
                AddVertex(cornerLeftX, -h, cornerLeftY, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0);
                AddVertex(cornerRightX, -h, cornerRightY, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0, -1.0);



            }

        }




        // event listener
        var mouseX = 0, mouseY = 0;
        var angle = [0.0, 0.0, 0.0, 1.0];
        var angleGL = 0;



        document.getElementById('gl')
.ProgramTest('mousemove', function(e)
    {
            if (e.buttons == 1)
            {
                // Left mouse button pressed
                angle[0] -= (mouseY - e.y) * 0.01;
                angle[1] += (mouseX - e.x) * 0.01;

                gl.uniform4fv(angleGL, new Float32Array(angle));
                Render();
            }
            mouseX = e.x;
            mouseY = e.y;

            // let coX = Math.cos(angle.x);
            // let siX = Math.sin(angle.x);
            // matX = mat4(vec4(1.0, 0.0, 0.0, 0.0),
            //                  vec4(0.0, coX, siX, 0.0),
            //                  vec4(0.0,-siX, coX, 0.0),
            //                  vec4(0.0, 0.0, 0.0, 1.0)
            //                  );
            // let coY = cos(Angle.y);
            // let siY = sin(Angle.y);
            // matY = mat4(vec4(coY, 0.0,-siY, 0.0),
            //                 vec4(0.0, 1.0, 0.0, 0.0),
            //                 vec4(siY, 0.0, coY, 0.0),
            //                 vec4(0.0, 0.0, 0.0, 1.0));





        }
);






        //function CreateGeometryUI()
        //{
        //    const ew= document.getElementById('w');
        //    const w = ew ? ew.value : 1.0;
        //    const eh = document.getElementById('h');
        //    const h = eh ? eh.value : 1.0;
        //    const ed = document.getElementById('d');
        //    const d = ed ? ed.value : 1.0;
        //    const sd = document.getElementById('sub');
        //    const s = sd ? sd.value : 5.0;

        //    document.getElementById('ui').innerHTML =
        //    'Width: <input type="number" id="w" value="'
        //    + w
        //    + '" onchange= "InitShaders();"><br>'

        //    + 'Height: <input type="number" id="h" value="'
        //    + h
        //    + '" onchange= "InitShaders();"><br>'

        //    + 'Depth: <input type="number" id="d" value="'
        //    + d
        //    + '" onchange= "InitShaders();">'

        //    + 'Subdivide: <input type="number" id="sub" value="'
        //    + s
        //    + '" onchange= "InitShaders();">';


        //    let e = document.getElementById('shape');
        //    switch (e.selectedIndex)
        //    {
        //        case 0:
        //            CreateQuad(w, h);

        //            break;

        //        case 1:
        //            CreateTriangle(w, h);
        //            break;

        //        case 2:
        //            CreateSubdividedBox(s, w, h, d);

        //            break;

        //        case 3:
        //            CreateBox(w, h, d);
        //            break;

        //        case 4:
        //            CreateCylinder(w, h, s);
        //            break;
        //    }


        //}



        function CreateTexture(prog, url)
        {
            // load tex to g-card
            const texture = LoadTexture(url);

            // flip y axis as pr norm
            gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);

            // activeate texture to tex unit 0
            gl.activeTexture(gl.TEXTURE0);
            gl.bindTexture(gl.TEXTURE_2D, texture);

            // add unrfomr location to fragment shader
            textureGL = gl.getUniformLocation(prog, 'Texture');

            // add uniform location to fragment shader
            displayGL = gl.getUniformLocation(prog, 'Display');
        }


        function LoadTexture(url)
        {
            const texture = gl.createTexture();
            gl.bindTexture(gl.TEXTURE_2D, texture);
            const pixel = new Uint8Array([0, 0, 255, 255]);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, 1, 1, 0, gl.RGBA, gl.UNSIGNED_BYTE, pixel);
            const image = new Image();
            image.onload = () =>
            {
                gl.bindTexture(gl.TEXTURE_2D, texture);
                gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
                SetTextureFilters(image);
            };

            image.src = url;
            return texture;
        }


        function SetTextureFilters(image)
        {
            if (IsPow2(image.width) && IsPow2(image.height))
            {
                gl.generateMipmap(gl.TEXTURE_2D);
            }
            else
            {
                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
            }
        }

        function IsPow2(value)
        {
            return (value & (value - 1)) === 0;
        }

        function Update()
        {
            // Show texture (boolean) last element
            const t = document.getElementById('t');
            display[3] = t.checked? 1.0 : 0.0;

            //light color *hex to rgb
            const l = document.getElementById('l').value;
            display[0] = parseInt(l.substring(1, 3), 16) / 255.0;
            display[1] = parseInt(l.substring(3, 5), 16) / 255.0;
            display[2] = parseInt(l.substring(5, 7), 16) / 255.0;



            // Update array to graphics card and render 
            gl.uniform4fv(displayGL, new Float32Array(display));
            Render();
            }


            function Perspective(fovy, aspect, near, far, matrix)
            {
                // fill array with zero;
                matrix.fill(0);
                // focal lenght;
                const f = Math.tan(fovy * Math.PI / 360.0);

                // setup matrix
                matrix[0] = f / aspect;
                matrix[5] = f;
                matrix[10] = (far + near) / (near - far);
                matrix[11] = (2 * far * near) / (near - far);
                matrix[14] = -1;

                gl.uniformMatrix4fv(proGL, false, new Float32Array(projection));
                gl.uniformMatrix4fv(modGL, false, new Float32Array(modelView));

            }
        }
    }
}
