using DTPortal.Core.Domain.Models.RegistrationAuthority;
using DTPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTPortal.Core.Persistence.Repositories
{
    public class ThresholdRepository : GenericRepository<FaceThreshold, ra_0_2Context>,
        IThresholdRepository
    {
        private readonly ILogger _logger;
        public ThresholdRepository(ra_0_2Context context,
            ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }
        public FaceThreshold GetThreshold()
        {
            try
            {
                return Context.FaceThresholds.FirstOrDefault(ss => ss.MethodName == "FACE_MATCH_THRESHOLD");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberDetails::Database exception");
                return null;
            }
        }
        public async Task<FaceThreshold> GetVoiceThreshold()
        {
            try
            {
                return await Context.FaceThresholds.AsNoTracking().SingleOrDefaultAsync(ss => ss.MethodName == "VOICE_MATCH_THRESHOLD");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "GetSubscriberDetails::Database exception");
                return null;
            }
        }
    }
}
