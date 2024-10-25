using Cutreson_PLC.McProtocol;
using Cutreson_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineStatusController : ControllerBase
    {
        private McProtocolTcp PLC;
        private readonly IConfiguration _configuration;
        public MachineStatusController(IConfiguration configuration)
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

        [HttpGet]
        public IActionResult Get()
        {
            if (Connect())
            {
                //Đọc 10 thanh ghi => 160 bit => đọc được từ M0 đến M159 trong lệnh này
                byte[] byteArr = PLC.ReadDeviceBlock(PlcDeviceType.M, 0, 10);
                bool[] M_Arr = clsRadixTransformation.ByteArrayToBoolArray(byteArr);

                MachineStatus machineStatus = new MachineStatus();
                machineStatus.Ready = M_Arr[0];
                machineStatus.HomeMachine = M_Arr[100];
                machineStatus.AutoMode = M_Arr[101];
                machineStatus.ManualMode = M_Arr[102];
                machineStatus.AutoRunning = M_Arr[103];
                machineStatus.Pause = M_Arr[104];
                machineStatus.OriginRunning = M_Arr[106];
                machineStatus.InitialRunning = M_Arr[109];
                machineStatus.HeavyAlarm = M_Arr[110];
                machineStatus.LightAlarm = M_Arr[111];
                machineStatus.DoorOpen = M_Arr[112];
                Disconnect();
                return Ok(new { message = "Get machine status successfully..!!", machineStatus });
            }
            else
            {
                return BadRequest("Connect to PLC failed");
            }
        }
    }
}
