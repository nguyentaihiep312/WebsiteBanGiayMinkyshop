using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Newtonsoft.Json;
using Serilog;
using Code = System.Net.HttpStatusCode;

namespace MinkyShopProject.Business.Repositories.ViDiem
{
    public class ViDiemRepository : IViDiemRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public ViDiemRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> CreateViDiem(ViDiemThemVaSuaModel model)
        {
            try
            {
                var entityModel = new Entities.ViDiem()
                {
                    SoDiemDaCong = model.SoDiemDaCong,
                    TongDiem = model.TongDiem,
                    SoDiemDaDung = model.SoDiemDaDung

                };

                _context.Add(entityModel);

                var status = await _context.SaveChangesAsync();
                if (status > 0)
                {
                    var data = _mapper.Map<Entities.ViDiem, ViDiemModel>(entityModel);
                    return new ResponseObject<ViDiemModel>(data, "Thêm thành công");
                }

                return new ResponseError(Code.BadRequest, "Thêm thất bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> UpdateViDiem(Guid Id, ViDiemThemVaSuaModel model)
        {
            try
            {
                var entityModel = await _context.ViDiem.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (entityModel == null)
                {
                    return new ResponseError(Code.BadRequest, "không tìm thấy Id ví điểm");
                }

                entityModel.SoDiemDaCong = model.SoDiemDaCong;
                entityModel.SoDiemDaDung = model.SoDiemDaDung;
                entityModel.TongDiem = model.TongDiem;

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.ViDiem, ViDiemModel>(entityModel);
                    return new ResponseObject<ViDiemModel>(data, "Thêm thành công");
                }

                return new ResponseError(Code.BadRequest, "Thêm thất bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> DeleteViDiem(Guid Id)
        {
            try
            {
                var entity = await _context.ViDiem.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return new ResponseError(Code.BadRequest, "Không tìm thấy Id ví điểm");
                }

                _context.Remove(entity);
                _context.SaveChanges();

                return new Response("Xóa thành công");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }


        public async Task<Response> GetViDiem(ViDiemQueryModel filter)
        {
            try
            {
                var result = _context.ViDiem.GetPage(filter);
                var ViDiemDto =
                    JsonConvert.DeserializeObject<Pagination<ViDiemModel>>(JsonConvert.SerializeObject(result));
                return new ResponsePagination<ViDiemModel>(ViDiemDto);
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu bộ phận không thành công");
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> GetById(Guid Id)
        {
            try
            {
                var entity = await _context.ViDiem.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return new ResponseError(Code.BadRequest, "Không tìm thấy Id ví điểm");
                }

                var data = _mapper.Map<Entities.ViDiem, ViDiemModel>(entity);
                return new ResponseObject<ViDiemModel>(data);
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu ví điểm thất bại");
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
        // private Expression<Func<Entities.ViDiem, bool>> BuildQueryViDiem(ViDiemQueryModel query)
        // {
        //     var predicate = PredicateBuilder.New<Entities.ViDiem>(true);
        //
        //     if (query.vidiemId.HasValue && query.vidiemId.Value != Guid.Empty)
        //     {
        //         predicate = predicate.And(c => c.Id == query.vidiemId);
        //     }
        //     return predicate;
        // }
    }
}
