using Cutreson_PLC.McProtocol;
using Cutreson_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelDownloadController : ControllerBase
    {
        private McProtocolTcp PLC;
        private readonly IConfiguration _configuration;

        public ModelDownloadController(IConfiguration configuration)
        {
            _configuration = configuration;
            string ipAddress = _configuration["PLCSettings:IPAddress"];
            int port = int.Parse(_configuration["PLCSettings:Port"]);
            PLC = new McProtocolTcp(ipAddress, port, "BINARY", "QSerise");
        }
        private bool Connect()
        {
            try
            {
                bool isOpened = PLC.Open();
                bool isConnected = PLC.IsConnect();
                return isConnected;
            }
            catch
            {
                return false;
            }
        }
        private bool Disconnect()
        {
            try
            {
                return PLC.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool WriteModel(TrayModel model)
        {
            try
            {
                PLC.WriteDeviceBlock(PlcDeviceType.ZR, 100000 + 300 * model.ModelNo, 84, model.ScrewType);
                PLC.WriteDeviceBlock(PlcDeviceType.ZR, 100100 + 300 * model.ModelNo, 84, model.IsGlue);
                PLC.WriteDeviceBlock(PlcDeviceType.ZR, 100200 + 300 * model.ModelNo, 10, clsRadixTransformation.StringToIntArray(model.ModelName, 10));
                int[] writeTime = new int[6];
                writeTime[0] = DateTime.Now.Year;
                writeTime[1] = DateTime.Now.Month;
                writeTime[2] = DateTime.Now.Day;
                writeTime[3] = DateTime.Now.Hour;
                writeTime[4] = DateTime.Now.Minute;
                writeTime[5] = DateTime.Now.Second;
                PLC.WriteDeviceBlock(PlcDeviceType.ZR, 100210 + 300 * model.ModelNo, 6, writeTime);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (PLC == null || !PLC.IsConnect())
            {
                if (Connect())
                {
                    List<TrayModel> models = new List<TrayModel>();
                    for (int i = 0; i < 100; i++)
                    {
                        byte[] byteArr = PLC.ReadDeviceBlock(PlcDeviceType.ZR, 100000 + i * 300, 300);
                        int[] dataRead = clsRadixTransformation.ByteArrayToInt16Array(byteArr);
                        if(dataRead.Length != 300) return BadRequest("Read data failed");
                        TrayModel model = new TrayModel();
                        //Model no.
                        model.ModelNo = i;
                        //Model name
                        int[] nameArr = new int[10];
                        Array.ConstrainedCopy(dataRead, 200, nameArr, 0, 10);
                        bool checkName = false;
                        for (int j = 0; j < 10; j++) if(nameArr[j] != 0) checkName = true;
                        string modelName = clsRadixTransformation.IntArrayToString(nameArr);
                        if (!checkName) continue;
                        model.ModelName = modelName;
                        //Screw type
                        Array.ConstrainedCopy(dataRead, 0, model.ScrewType, 0, 84);
                        //Glue
                        Array.ConstrainedCopy(dataRead, 100, model.IsGlue, 0, 84);
                        //
                        models.Add(model);
                    }
                    Disconnect();
                    return Ok(new { message = "Get all models successfully..!!", models });
                }
                else
                {
                    return BadRequest("Connect failed");
                }
            }
            else
            {
                return BadRequest("Connect failed");
            }
        }

        [HttpGet("CheckConnect")]
        public IActionResult GetCheckConnect()
        {
            if(PLC == null || !PLC.IsConnect())
            {
                if(Connect())
                {
                    Disconnect();
                    return Ok("Connected");
                }
                else
                {
                    return BadRequest("Connect failed");
                }
            }
            else
            {
                return BadRequest("Connect failed");
            } 
        }


        [HttpPost("WriteSingleModel")]
        public IActionResult WriteSingleModel([FromBody] TrayModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Model is null." });
            }
            if (string.IsNullOrEmpty(model.ModelName))
            {
                return BadRequest(new { message = "ModelName is required.", model });
            }
            if (model.ModelNo >= 100)
            {
                return BadRequest(new { message = "ModelNo must be less than 100.", model });
            }
            if (model.ScrewType.Length != 84)
            {
                return BadRequest(new { message = "ScrewType must have exactly 84 elements.", model });
            }
            if (model.IsGlue.Length != 84)
            {
                return BadRequest(new { message = "IsGlue must have exactly 84 elements.", model });
            }

            try
            {
                if (Connect() == false) return BadRequest("Can't connect to PLC");
                if(WriteModel(model))
                {
                    Disconnect();
                    return Ok(new { message = "Download model successfully", model });
                }
                else
                {
                    Disconnect();
                    return BadRequest("Download model error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Download model error: " + ex.Message);
            }
            
        }

        [HttpPost("WriteAllModel")]
        public IActionResult WriteAllModel([FromBody] List<TrayModel> models)
        {
            if (models == null || models.Count != 100)
            {
                return BadRequest(new { message = "Exactly 100 models are required." });
            }

            for (int i = 0; i < models.Count; i++)
            {
                var model = models[i];

                if (model == null)
                {
                    return BadRequest(new { message = $"Model at index {i} is null." });
                }
                if (string.IsNullOrEmpty(model.ModelName))
                {
                    return BadRequest(new { message = $"ModelName is required for model at index {i}.", model });
                }
                if (model.ModelNo >= 100)
                {
                    return BadRequest(new { message = $"ModelNo must be less than 100 for model at index {i}.", model });
                }
                if (model.ScrewType.Length != 84)
                {
                    return BadRequest(new { message = $"ScrewType must have exactly 84 elements for model at index {i}.", model });
                }
                if (model.IsGlue.Length != 84)
                {
                    return BadRequest(new { message = $"IsGlue must have exactly 84 elements for model at index {i}.", model });
                }
            }

            try
            {
                if (!Connect())
                {
                    return BadRequest("Can't connect to PLC");
                }

                for (int i = 0; i < models.Count; i++)
                {
                    var model = models[i];
                    if (!WriteModel(model))
                    {
                        Disconnect();
                        return BadRequest(new { message = $"Error writing model at index {i}.", model });
                    }
                }

                Disconnect();
                return Ok(new { message = "Download all models successfully", models });
            }
            catch (Exception ex)
            {
                Disconnect();
                return BadRequest(new { message = $"Download all models error: {ex.Message}" });
            }
        }
    }
}
