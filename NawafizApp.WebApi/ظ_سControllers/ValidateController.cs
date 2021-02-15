using NawafizApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NawafizApp.WebApi.Controllers
{
    //[RoutePrefix("api/Validation")]
    public class ValidationController : ApiController
    {
        private readonly IUserService _userService;
       
        private readonly ILanguageService _languageService;

        public ValidationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("~/api/Validate/IsEmailUnique")]
        public async Task<IHttpActionResult> IsEmailUnique(string email)
        {
            bool validationResult = false;

            if (!String.IsNullOrEmpty(email))
            {
                validationResult = _userService.IsEmailUnique(email);
            }

            return Ok(validationResult);
        }



        


        //#region GuideCityValidate

        //[HttpGet]
        //public async Task<IHttpActionResult> GuideIsArabicNameAddUnique(string ArabicCityName)
        //{
        //    bool validationResult = false;

        //    if (!String.IsNullOrEmpty(ArabicCityName))
        //    {



        //        validationResult = _guideCityService.IsNameUnique(ArabicCityName, null);
        //    }





        //    return Ok(validationResult);
        //}



        //    [HttpGet]
        //public async Task<IHttpActionResult> GuideIsEnglishNameAddUnique(string EnglishCityName)
        //{
        //    bool validationResult = false;

        //    if (!String.IsNullOrEmpty(EnglishCityName))
        //    {



        //        validationResult = _guideCityService.IsNameUnique(EnglishCityName, null);
        //    }




        //    return Ok(validationResult);
        //}











        //      [HttpGet]
        //public async Task<IHttpActionResult> GuideIsArabicNameEditUnique(string ArabicCityName,int Id)
        //{
        //    bool validationResult = false;




        //    if (!String.IsNullOrEmpty(ArabicCityName) && Id != null)
        //    {




        //        validationResult = _guideCityService.IsNameUnique(ArabicCityName, Id);
        //    }

        //    return Ok(validationResult);
        //}

        //    [HttpGet]
        //public async Task<IHttpActionResult> GuideIsEnglishNameEditUnique(string EnglishCityName,int Id)
        //{
        //    bool validationResult = false;

        //    if (!String.IsNullOrEmpty(EnglishCityName) && Id != null)
        //    {
        //        validationResult = _guideCityService.IsNameUnique(EnglishCityName, Id);
        //    }


        //    return Ok(validationResult);
        //}




        //    [HttpGet]
        //public async Task<IHttpActionResult> GuideIsExistIdEditUnique(int Id)
        //{
        //    bool validationResult = false;

        //    if (Id != null)
        //    {



        //        validationResult = _guideCityService.IsExistId(Id);
        //    }




        //    return Ok(validationResult);
        //}

        //#endregion


        //#region GuideTownValidate
        //[HttpGet]
        //public async Task<IHttpActionResult> GuideIsCityIdExist(int CityId)
        //{
        //    bool validationResult = false;

        //    if (CityId != null)
        //    {

        //        validationResult = _guideTownService.IsCityIdExist(CityId);
        //    }

        //    return Ok(validationResult);
        //}

        //[HttpGet]
        //public async Task<IHttpActionResult> GuideIsIdExistTown(int Id)
        //{
        //    bool validationResult = false;

        //    if (Id != null)
        //    {

        //        validationResult = _guideTownService.IsIdExist(Id);
        //    }

        //    return Ok(validationResult);
        //}

        //#endregion



     


        //#region GuideFavoriteValidate

        //    [HttpGet]
        //public async Task<IHttpActionResult> IsGuideClassifyIdExist_InClassify(int Id)
        //{
        //    bool validationResult = false;

        //    if (Id != null)
        //    {

        //        validationResult = _guideFavoriteService.IsClassifyIdExist_InGuideClassify(Id);
        //    }

        //    return Ok(validationResult);
        //}
        //#endregion

        //#region GuideValidate

        //    [HttpGet]
        //public async Task<IHttpActionResult> IsIdExist_Guide(int Id)
        //{
        //    bool validationResult = false;

        //    if (Id != null)
        //    {

        //        validationResult = _guideService.IsIdExist(Id);
        //    }

        //    return Ok(validationResult);
        //}

        //[HttpGet]
        //public async Task<IHttpActionResult> IsLevelThree(int GuideId)
        //{
        //    bool validationResult = true;

        //    if (GuideId != null)
        //    {

        //        validationResult = !_guideService.IsLevelThree(GuideId);
        //    }

        //    return Ok(validationResult);
        //}


        //[HttpGet]
        //public async Task<IHttpActionResult> IsNameUniqueAdd(string ArabicName, string EnglishName)
        //{
        //    bool validationResult = true;

        //    if (!String.IsNullOrEmpty(ArabicName) && !String.IsNullOrEmpty(EnglishName))
        //    {

        //        validationResult = (_guideService.IsNameUnique(ArabicName, null) && _guideService.IsNameUnique(EnglishName, null));
        //    }

        //    return Ok(validationResult);
        //}



        //[HttpGet]
        //public async Task<IHttpActionResult> IsNameUniqueEdit(string ArabicName, string EnglishName, int Id)
        //{
        //    bool validationResult = true;

        //    if (!String.IsNullOrEmpty(ArabicName) && !String.IsNullOrEmpty(EnglishName) && Id != null)
        //    {

        //        validationResult = (_guideService.IsNameUnique(ArabicName, Id) && _guideService.IsNameUnique(EnglishName, Id));
        //    }

        //    return Ok(validationResult);
        //}


        //[HttpGet]
        //public async Task<IHttpActionResult> IsParentExist(int ParentId)
        //{
        //    bool validationResult = true;

        //    if (ParentId != null)
        //    {

        //        validationResult = _guideService.IsParentExist(ParentId);
        //    }

        //    return Ok(validationResult);
        //}



        //[HttpGet]
        //public async Task<IHttpActionResult> ParentID_equal_Id(int ParentId, int Id)
        //{
        //    bool validationResult = true;

        //    if (ParentId != null && Id != null)
        //    {

        //        validationResult = ParentId != Id;
        //    }

        //    return Ok(validationResult);
        //}

        //#endregion

        //#region LanguageValidate

        //     [HttpGet]
        //public async Task<IHttpActionResult> IsCodeUniqueAdd(string code)
        //{
        //    bool validationResult = true;

        //    if (!String.IsNullOrEmpty(code))
        //    {

        //        validationResult = _languageService.IsCodeUnique(code,null);
        //    }

        //    return Ok(validationResult);
        //}


        //#endregion

    }
}
