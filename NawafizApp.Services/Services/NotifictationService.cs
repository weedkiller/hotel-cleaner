using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class NotifictationService : INotifictationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotifictationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public int Add(NotifictationDto dto)
        {
            Notifictation n = new Notifictation();

           
            n.senderId = dto.senderId.Value;
            n.RevieverId = dto.RevieverId.Value;
            n.NotText = dto.NotText;
            n.NotDateTime = dto.NotDateTime;
            if (dto.Room_ID.HasValue)
            {
                n.Room_ID = dto.Room_ID;
            }
            if (dto.Equipment_ID.HasValue)
            {
                n.Equipment_ID = dto.Equipment_ID;
            }
            _unitOfWork.NotifictationRepository.Add(n);
            _unitOfWork.SaveChanges();
            return n.Id;
        
        }

        public List<NotifictationDto> All(Guid? userid)
        {
            NotifictationDto dto = new NotifictationDto();
            List<NotifictationDto> listdto = new List<NotifictationDto>();
            var list = _unitOfWork.NotifictationRepository.GetAll().Where(x =>x.RevieverId==userid.Value).ToList();
            foreach (var item in list)
            {
                dto.Id = item.Id;
                dto.SenderName = _unitOfWork.UserRepository.FindById(item.senderId).UserName;
                dto.RevieverName = _unitOfWork.UserRepository.FindById(item.RevieverId).UserName;
                dto.IsRead = item.IsRead;
                if (item.NotText.Contains('&'))
                {
                    var x = item.NotText.Split('&');
                    dto.NotText = x[0];
                    dto.orderId = x[1];
                }
                else
                {
                    dto.NotText = item.NotText;
                }
                dto.NotDateTime = item.NotDateTime;
                dto.RevieverId = item.RevieverId;
                dto.Room_ID = item.Room_ID;
                dto.senderId = item.senderId;
                listdto.Add(dto);
                dto = new NotifictationDto();
            }
            return listdto;
      
        }

        public bool Edit(NotifictationDto dto)
        {
            Notifictation n = _unitOfWork.NotifictationRepository.FindById(dto.Id);

            n.IsRead = dto.IsRead;

            _unitOfWork.NotifictationRepository.Update(n);
            _unitOfWork.SaveChanges();
            return true;
         
        }

        public NotifictationDto GetById(int id)
        {
            NotifictationDto dto = new NotifictationDto();

            var item = _unitOfWork.NotifictationRepository.FindById(id);
         
                dto.Id = item.Id;
                dto.SenderName = _unitOfWork.UserRepository.FindById(item.senderId).UserName;
                dto.RevieverName = _unitOfWork.UserRepository.FindById(item.RevieverId).UserName;
                dto.IsRead = item.IsRead;
            if (item.NotText.Contains('&'))
            {
                var x = item.NotText.Split('&');
                dto.NotText = x[1];
                dto.orderId = x[0];
            }
            else
            {
                dto.NotText = item.NotText;
            }
                     dto.Room_ID = item.Room_ID;
          
                dto.NotDateTime = item.NotDateTime;
                dto.RevieverId = item.RevieverId;
                dto.senderId = item.senderId;
              
              
         
            return dto;
        }

        public bool NotifictationRemove(int Id)
        {
            var n = _unitOfWork.NotifictationRepository.FindById(Id);
            if (n == null)
            {
                return false;
            }
     
            _unitOfWork.NotifictationRepository.Remove(n);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
