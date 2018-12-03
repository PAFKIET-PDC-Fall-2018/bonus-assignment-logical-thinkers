
//Code by [Monis Azhar(59485)]

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace BonusAssignmentWebApi.Controllers
{

    

    [RoutePrefix("api/videos")]
    public class VideosController : ApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetVideoContent()
        {
            var httpResponse = Request.CreateResponse();

            httpResponse.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream, new MediaTypeHeaderValue("video/" + "mp4"));

            return httpResponse;
        }

        public async void WriteContentToStream(Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            string url = string.Empty;

            //path of file which we have to read//  
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "/videos/India - First Ever Visited India - What Happened to Molana Tariq Jameel latest bayan 26-Nov-2018.mp4";
            //here set the size of buffer, you can set any size  
            int bufferSize = 65535;
            byte[] buffer = new byte[bufferSize];

            try
            {
                //here we re using FileStream to read file from server//  
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    int totalSize = (int)fileStream.Length;
                    /*here we are saying read bytes from file as long as total size of file 

                    is greater then 0*/
                    while (totalSize > 0)
                    {
                        int count = totalSize > bufferSize ? bufferSize : totalSize;
                        //here we are reading the buffer from orginal file  
                        int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
                        //here we are writing the readed buffer to output//  
                        await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
                        //and finally after writing to output stream decrementing it to total size of file.  
                        totalSize -= sizeOfReadedBuffer;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}

//Code by [Monis Azhar(59485)]