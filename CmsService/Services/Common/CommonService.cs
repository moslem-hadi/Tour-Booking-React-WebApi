using CmsApiService.Entities;
using CmsApiService.Models; 
using CmsApiService.Services.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
    public class CommonService: ICommonService
    { 

        private readonly IDapperService<object> _dapperService;
        private readonly IDapperService<SystemLog> _logService;
        private IConfiguration _configuration { get; }
        private readonly ILogger<ProductService> _logger;
        public CommonService(IConfiguration configuration, ILogger<ProductService> logger, IDapperService<object> dapperService, 
            IDapperService<SystemLog> logService)
        {
            _configuration = configuration;
            _dapperService = dapperService;
            _logger = logger;
            _logService = logService;
        }

         
        public List<AdvertismentBasicViewModel> GetPictures(AdvertismentQueryViewModel query)
        {
            try
            {
                string sql = $@"
                            SELECT  {(query.Count==0 ? "" : "top(@count)")} [ID],[Title],[Pic],[Link],[ColumnSize],[Priority]
                            FROM [dbo].[advertisment] where place = @place";

                var ads = _dapperService.Query<AdvertismentBasicViewModel>(sql, new
                {
                    count = query.Count,
                    place = (int)query.Place
                }).ToList();

                return ads;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public FirstPageDataViewModel GetFirstPageData()
        {
            try
            {
                FirstPageDataViewModel result = new FirstPageDataViewModel();
                List<GlobalValueEntity> globalValues = new List<GlobalValueEntity>();
                string sql = "FirstPageInfo";


                using (var connection = _dapperService.GetConnection())
                {
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();

                    using (var multi = connection.QueryMultiple(sql, commandType: System.Data.CommandType.StoredProcedure))
                    {
                        result = multi.Read<FirstPageDataViewModel>().First();
                        globalValues = multi.Read<GlobalValueEntity>().ToList();
                        result.Cities = multi.Read<TitleValueViewModel>().ToList();
                    }
                }
                result.ActivityTypes = globalValues.Where(a => a.Type == GlobalValueTypes.ActivityType).Select(a => new TitleValueViewModel
                {
                    Title = a.Title,
                    Value = a.Value
                }).ToList();



                result.HamrahiTypes = globalValues.Where(a => a.Type == GlobalValueTypes.Hamrahi).Select(a => new TitleValueViewModel
                {
                    Title = a.Title,
                    Value = a.Value
                }).ToList();

                result.PlaceTypes = globalValues.Where(a => a.Type == GlobalValueTypes.Place).Select(a => new TitleValueViewModel
                {
                    Title = a.Title,
                    Value = a.Value
                }).ToList();

                result.VehicleTypes = globalValues.Where(a => a.Type == GlobalValueTypes.Vehicle).Select(a => new TitleValueViewModel
                {
                    Title = a.Title,
                    Value = a.Value
                }).ToList();

                result.ActivityTypes = globalValues.Where(a => a.Type == GlobalValueTypes.ActivityType).Select(a => new TitleValueViewModel
                {
                    Title = a.Title,
                    Value = a.Value
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<MenuLink> GetMenuLinks(MenuPosition position)
        {
            try
            {
                string sql = $@"select * from menulinks where Position = @position order by Priority";

                var menus = _dapperService.Query<MenuLink>(sql, new
                { 
                    position = (int)position
                }).ToList();

                return menus;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageViewModel GetPage(string slug)
        {
            try
            {
                string sql = $@"select * from pagecontent where [short]=@slug";

                var page = _dapperService.Query<PageViewModel>(sql, new
                {
                    slug = slug.ToLower().Trim()
                }).FirstOrDefault();

                return page;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool SubmitContactUs(ContactUsForm model)
        {
            try
            {
                string sql = @"INSERT INTO [dbo].[contactPm] ([FulName],[Email],[Tell],[Title],[Text],[Reply],[IsRead],[RegDate],[UserID],[IP])
                            VALUES(@name,'',@mobile,N'پیام از فرم تماس با ما',@message,'',0,GetDate(),0,'')";

                _dapperService.Execute(sql, System.Data.CommandType.Text, model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddSystemLog(SystemLogViewModel model)
        {
            _logService.Insert(new SystemLog()
            {
                Exception = model.Exception,
                Message = model.Message,
                Type = model.Type,
                Url = model.Url
            });
        }

        public UserBasicViewModel GetUserByAuthToken(string token)
        {
            try
            {
                string sql = $@"select u.Id, u.FullName,u.Mobile from LoginTokens t 
                                join usersdata u on t.UserId=u.ID
                                where t.Token=@token and t.ExpireDate>GETDATE()";

                var user = _dapperService.Query<UserBasicViewModel>(sql, new
                {
                    token = token
                }).FirstOrDefault();

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
