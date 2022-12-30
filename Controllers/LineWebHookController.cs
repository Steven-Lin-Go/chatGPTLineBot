using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace isRock.Template
{
    public class LineWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        [Route("api/LineBotWebHook")]
        [HttpPost]
        public IActionResult POST()
        {
            var AdminUserId = "__________U15ef34de67ebaa8b0556a0d1b69f23c1________________";

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "______________jyY4mZqNgPpIpZQRfZLI4g4HovrHFk0f4ogvGTedVDfrbFb8gy391aW5IETaXbUAx5uQd5krmM4oJF4jMZHHB4alFEQJ8UwSMrZg273SKfcV1XdeYP5Re+mTXXnj//rMgD3C1/BgMNprNp8agjiZAgdB04t89/1O/w1cDnyilFU=______________";
                //配合Line Verify
                if (ReceivedMessage.events == null || ReceivedMessage.events.Count() <= 0 ||
                    ReceivedMessage.events.FirstOrDefault().replyToken == "00000000000000000000000000000000") return Ok();
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                var responseMsg = "";
                //準備回覆訊息
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                    responseMsg = $"{  isRock.Template.ChatGPT.CallChatGPT(LineEvent.message.text).choices.FirstOrDefault().text}";
                else if (LineEvent.type.ToLower() == "message")
                    responseMsg = $"收到 event : {LineEvent.type} type: {LineEvent.message.type} ";
                else
                    responseMsg = $"收到 event : {LineEvent.type} ";
                //回覆訊息
                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
