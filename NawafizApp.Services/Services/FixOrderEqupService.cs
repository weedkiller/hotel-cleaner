using AutoMapper;
using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Services.Dtos;
using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Services.Services
{
    public class FixOrderEqupService : IFixOrderEqupService
    {
        IUnitOfWork _unitOfWork;
        public FixOrderEqupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public int add(FixOrderEqupDto fixOrderEqupDto, int orderid)
        {
            FIxOrderEqupment fIxOrderEqupment = new FIxOrderEqupment();
            fIxOrderEqupment.Name = fixOrderEqupDto.Name;
            fIxOrderEqupment.fixOrder = _unitOfWork.FixOrderRepository.FindById(orderid);

            _unitOfWork.FIxOrderEqupment.Add(fIxOrderEqupment);
            _unitOfWork.SaveChanges();
            return fixOrderEqupDto.Id;

        }


        public List<FixOrderEqupDto> All(int oid)
        {


            FixOrderEqupDto equpDto = new FixOrderEqupDto();
            var list = _unitOfWork.FixOrderRepository.FindById(oid).FIxOrderEqupment;
            List<FixOrderEqupDto> vd = new List<FixOrderEqupDto>();
            foreach (var item in list)

            {

                equpDto.Id = item.Id;
                equpDto.Name = item.Name;

                vd.Add(equpDto);
                equpDto = new FixOrderEqupDto();

            }
            


            return vd;




        }
    }
}
