//using Application.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class DeliveryService : IDeliveryService
//    {
//        private readonly ISmsService _smsService;
//        private readonly IWhatsAppService _whatsAppService;
//        private readonly IMapper _mapper;

//        public DeliveryService(
//            ISmsService smsService,
//            IWhatsAppService whatsAppService,
//            IMapper mapper)
//        {
//            _smsService = smsService;
//            _whatsAppService = whatsAppService;
//            _mapper = mapper;
//        }

//        public async Task<ServiceResult> RetryFailedDeliveriesAsync(Delivery delivery)
//        {
//            try
//            {
//                var failedRecipients = delivery.Recipients
//                    .Where(r => r.Status == DeliveryStatus.Failed)
//                    .ToList();

//                foreach (var recipient in failedRecipients)
//                {
//                    var result = delivery.Channel switch
//                    {
//                        DeliveryChannel.SMS => await _smsService.ResendSurveyAsync(
//                            recipient.ContactInfo,
//                            delivery.Survey),

//                        DeliveryChannel.WhatsApp => await _whatsAppService.ResendSurveyAsync(
//                            recipient.ContactInfo,
//                            delivery.Survey),

//                        _ => throw new InvalidOperationException("Unsupported delivery channel")
//                    };

//                    if (result.Success)
//                    {
//                        recipient.Status = DeliveryStatus.Pending;
//                        recipient.LastAttemptDate = DateTime.UtcNow;
//                    }
//                }

//                return ServiceResult.Success("Retry attempts initiated");
//            }
//            catch (Exception ex)
//            {
//                return ServiceResult.Failure($"Failed to retry deliveries: {ex.Message}");
//            }
//        }

//        public ExportFileResult GenerateExportFile(IReadOnlyList<Delivery> deliveries, string format)
//        {
//            return format.ToLower() switch
//            {
//                "csv" => GenerateCsvExport(deliveries),
//                "pdf" => GeneratePdfExport(deliveries),
//                _ => throw new ArgumentException("Unsupported export format")
//            };
//        }

//        private ExportFileResult GenerateCsvExport(IReadOnlyList<Delivery> deliveries)
//        {
//            // Implementation using CsvHelper
//        }

//        private ExportFileResult GeneratePdfExport(IReadOnlyList<Delivery> deliveries)
//        {
//            // Implementation using iTextSharp or DinkToPdf
//        }
//    }
//}
