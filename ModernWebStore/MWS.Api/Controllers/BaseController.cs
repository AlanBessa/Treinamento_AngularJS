﻿using MWS.SharedKernel;
using MWS.SharedKernel.Events;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MWS.Api.Controllers
{
    public class BaseController : ApiController
    {
        public IHandler<DomainNotification> Notifications;
        public HttpResponseMessage ResponseMessage;

        public BaseController()
        {
            Notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
            ResponseMessage = new HttpResponseMessage();
        }

        public Task<HttpResponseMessage> CreateResponse(HttpStatusCode code, object result)
        {
            if (Notifications.HasNotifications())
            {
                ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = Notifications.Notify() });
            }
            else
            {
                ResponseMessage = Request.CreateResponse(code, result);
            }

            return Task.FromResult<HttpResponseMessage>(ResponseMessage);
        }

        protected override void Dispose(bool disposing)
        {
            Notifications.Dispose();
        }
    }
}