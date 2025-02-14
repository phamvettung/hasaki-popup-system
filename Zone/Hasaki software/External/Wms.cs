using Intech_software.Models.WmsModel;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intech_software.External
{
    internal class Wms
    {
        public async Task<CreatePartnerOrderResponse> CreatePartnerOrder(CreatePartnerOrderRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var token = UserLogin.Token; // System.Configuration.ConfigurationSettings.AppSettings["WMS_TOKEN"];
                    var addHeaderSuccess = client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    var jsonRequest = JsonSerializer.Serialize(request);
                    var bodyRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    var uri = System.Configuration.ConfigurationSettings.AppSettings["INS_URL"] + "/api/delivery/shipping-order/partner-order";

                    using (HttpResponseMessage response = await client.PostAsync(uri, bodyRequest))
                    {
                        using (HttpContent content = response.Content)
                        {
                            try
                            {
                                string data = await content.ReadAsStringAsync();

                                var result = JsonSerializer.Deserialize<CreatePartnerOrderResponse>(data);

                                return result;
                            }
                            catch (Exception ex)
                            {
                                return new CreatePartnerOrderResponse()
                                {
                                    Message = ex.Message,
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new CreatePartnerOrderResponse()
                    {
                        Message = ex.Message,
                    };
                }
            }
        }

        public async Task<AddPartnerOrderItemsResponse> AddPartnerOrderItem(AddPartnerOrderItemsRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var token = UserLogin.Token;
                    var addHeaderSuccess = client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    var jsonRequest = JsonSerializer.Serialize(request);
                    var bodyRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    var uri = System.Configuration.ConfigurationSettings.AppSettings["INS_URL"] + "/api/delivery/shipping-order/partner-order/items/add";

                    using (HttpResponseMessage response = await client.PostAsync(uri, bodyRequest))
                    {
                        using (HttpContent content = response.Content)
                        {
                            try
                            {
                                string data = await content.ReadAsStringAsync();

                                var result = JsonSerializer.Deserialize<AddPartnerOrderItemsResponse>(data);
                                return result;
                            }
                            catch (Exception ex)
                            {
                                return new AddPartnerOrderItemsResponse()
                                {
                                    Message = ex.Message,
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new AddPartnerOrderItemsResponse()
                    {
                        Message = ex.Message,
                    };
                }
            }
        }

        public async Task<GetInfoUserResponse> Login(LoginRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var jsonRequest = JsonSerializer.Serialize(request);
                    var bodyRequest = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    var uri = System.Configuration.ConfigurationSettings.AppSettings["WMS_URL"] + "/api/v1/auth/user/login";

                    using (HttpResponseMessage response = await client.PostAsync(uri, bodyRequest))
                    {
                        using (HttpContent content = response.Content)
                        {
                            try
                            {
                                string data = await content.ReadAsStringAsync();

                                var result = JsonSerializer.Deserialize<GetInfoUserResponse>(data);
                                return result;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
    }
}
