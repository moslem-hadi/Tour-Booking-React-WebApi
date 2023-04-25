using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Manage
{
    public partial class setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CMS.Manage.managemaster MasterPage = (CMS.Manage.managemaster)Page.Master;
            MasterPage.Check_User();

            if (!IsPostBack)
            {
                using (DataAccessDataContext _data = new DataAccessDataContext())
                {
                    foreach (settingValue item in _data.settingValues)
                    {
                        switch (item.Short.ToLower())
                        {
                            case "websitetitle":
                                txtTitle.Text = item.ShortValue;
                                break;
                            case "sliderpic":
                                txtSliderPic.Text = item.ShortValue;
                                break;
                            case "slidertitle":
                                txtslidertitle.Text = item.ShortValue;
                                break;
                            case "slidersubtitle":
                                txtslidersubtitle.Text = item.ShortValue;
                                break;
                            case "slidertext":
                                txtslidertext.Text = item.ShortValue;
                                break;

                             
                            case "websitedescription":
                                txtDesc.Text = item.ShortValue;
                                break;

                            case "managermail":
                                txtManageMail.Text = item.ShortValue;
                                break;


                            case "websitename":
                                txtName.Text = item.ShortValue;
                                break;

                            case "websitemail":
                                txtEmail.Text = item.ShortValue;
                                break;

                            case "websitemailpass":
                                txtMailPass.Text = item.ShortValue;
                                break; 
                            case "mailservice":
                                txtMailservice.Text = item.ShortValue;
                                break;

                            case "mailserviceport":
                                txtMailserviceport.Text = item.ShortValue;
                                break;
                                 
                            //case "logourl":
                            //    txtLogo.Text = item.ShortValue;
                            //    break;
                                 
                            //case "contactus":
                            //    txtContactUs.Text = item.TextValue;
                            //    break;
                            case "daybulletin":
                                txtBulletin.Text = item.TextValue;
                                break;

                            case "firstpageheader":
                                txtFistPageHeader.Text = item.ShortValue;
                                break;



                            case "userpassword":
                                txtUserPassword.Text = item.ShortValue;
                                break;

                            case "username":
                                txtUserName.Text = item.ShortValue;
                                break;

                            case "terminalid":
                                txtTerminalId.Text = item.ShortValue;
                                break;

                            case "zarinpalmerchant":
                                txtZarin.Text = item.ShortValue;
                                break;



                            case "headcontent":
                                txtHeadContent.Text = item.ShortValue;
                                break;
                                 
                            case "footercontent":
                                txtFooter.Text = item.TextValue;
                                break;
                            case "footercontact":
                                txtFooterContact.Text = item.TextValue;
                                break;
                            case "firstpagewelcome":
                                txtFirstPageWelcome.Text = item.TextValue;
                                break;

                            case "sidebartext":
                                txtsideBar.Text = item.TextValue;
                                break;
                            case "smstobuyer":
                                txtsmstext.Text = item.ShortValue;
                                break;

                            case "managermob":
                                txtsmsManagermob.Text = item.ShortValue;
                                break;

                            case "smsnumber":
                                txtsmsnumber.Text = item.ShortValue;
                                break;
                                 
                            case "smssignature":
                                txtsign.Text = item.ShortValue;
                                break;






                            default:
                                break;
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (settingValue item in _data.settingValues.Where(a => a.Short == "websitetitle" ||
                       a.Short == "sliderpic" || a.Short == "websitedescription"
                       || a.Short == "logourl" || a.Short == "managermail" || a.Short == "slidertext"
                          || a.Short == "slidertitle" || a.Short == "slidersubtitle"))
                {
                  
                    switch (item.Short.ToLower())
                    {
                        case "websitetitle":
                            item.ShortValue = txtTitle.Text;
                            break;
                        case "sliderpic":
                            item.ShortValue = txtSliderPic.Text;
                            break; 
                        case "slidertext": 
                            item.ShortValue = txtslidertext.Text;
                            break;

                        case "slidertitle": 
                            item.ShortValue = txtslidertitle.Text;
                            break;
                        case "slidersubtitle":
                            item.ShortValue= txtslidersubtitle.Text;
                            break;
                        case "managermail":
                            item.ShortValue = txtManageMail.Text;
                            break;

                        case "websitedescription":
                            item.ShortValue = txtDesc.Text;
                            break; 
                        default:
                            break;
                    }
                    _data.SubmitChanges();
                }
            }
            SystemConfig.ClearInstance();
            Response.Redirect("setting.aspx#tabs-2");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int port = 25;
            int.TryParse(txtMailserviceport.Text, out port);
            if (port == 0) port = 25;
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {

                foreach (settingValue item in _data.settingValues.Where(a => a.Short == "websitename" ||
                    a.Short == "websitemail" || a.Short == "websitemailpass" || a.Short == "mailservice" || a.Short == "mailserviceport" ))
                {
                    switch (item.Short.ToLower())
                    {
                        case "websitename":
                            item.ShortValue = txtName.Text;
                            break;

                        case "websitemail":
                            item.ShortValue = txtEmail.Text;
                            break;

                        case "websitemailpass":
                            item.ShortValue = txtMailPass.Text;
                            break;

                        case "mailservice":
                            item.ShortValue = txtMailservice.Text;
                            break;

                        case "mailserviceport":
                            item.ShortValue = port.ToString();
                            break;


                        default:
                            break;
                    }
                    _data.SubmitChanges();
                }
            }
            SystemConfig.ClearInstance();
            Response.Redirect("setting.aspx#tabs-3");
        }

        //protected void Button3_Click(object sender, EventArgs e)
        //{

        //    using (DataAccessDataContext _data = new DataAccessDataContext())
        //    {
        //        foreach (SpecificationAttribute item in _data.SpecificationAttributes.Where(a => a.GroupID == 2509 && a.ID >= 64))
        //        {
        //            foreach (SpecificationAttributeOption specOptions in _data.SpecificationAttributeOptions.Where(a=>a.SpecificationAttributeID==item.ID ))
        //            {
        //                int id = _data.SpecificationAttributes.Single(a => a.GroupID == 2511 && a.FiledType==(byte)item.FiledType && a.Title == item.Title).ID;
        //                SpecificationAttributeOption tmp = new SpecificationAttributeOption();
        //                tmp.Priority = specOptions.Priority;
        //                tmp.SpecificationAttributeID = id;
        //                tmp.Title = specOptions.Title;
        //                _data.SpecificationAttributeOptions.InsertOnSubmit(tmp);
        //                _data.SubmitChanges();
        //            }
        //        }
        //    }
        //}
         

            //متون
        protected void Button4_Click(object sender, EventArgs e)
        {

            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                if(txtHeadContent.Text.Length>500 || txtFistPageHeader.Text.Length > 500)
                {
                    MessageBox3.Message = "تعداد حروف محتوای هدر اول و همه، حداکثر 500 حرف است.";
                    MessageBox3.Visible = true;
                    return;
                }
                foreach (settingValue item in _data.settingValues.Where(a => a.Short == "firstpageheader"
                || a.Short == "DayBulletin" ||
                a.Short == "HeadContent" ||
                     a.Short == "FooterContent" ||
                     a.Short == "FooterContact" ||
                     a.Short == "FirstPageWelcome" ||
                       a.Short == "sidebartext" ))
                {

                    switch (item.Short.ToLower())
                    { 
                        case "sidebartext":
                            item.TextValue = txtsideBar.Text;
                            break;
                        case "daybulletin":
                            item.TextValue = txtBulletin.Text;
                            break;
                        case "firstpageheader":
                            item.ShortValue = txtFistPageHeader.Text;
                            break;

                        case "headcontent":
                            item.ShortValue = txtHeadContent.Text;
                            break;

                        case "footercontact":
                            item.TextValue = txtFooterContact.Text;
                            break;
                        case "footercontent":
                            item.TextValue = txtFooter.Text;
                            break;
                        case "firstpagewelcome":
                            item.TextValue = txtFirstPageWelcome.Text;
                            break;


                    }
                    _data.SubmitChanges();
                }
            }
            Response.Redirect("setting.aspx#tabs-4");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
           


            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (settingValue item in _data.settingValues.Where(a => 
                     a.Short == "TerminalId" || a.Short == "UserName" || a.Short == "UserPassword" ||  a.Short == "zarinpalmerchant")
                     )
                {
                    switch (item.Short)
                    {
                       
                        case "UserPassword":
                            item.ShortValue = txtUserPassword.Text.ToString();
                            break;

                        case "UserName":
                            item.ShortValue = txtUserName.Text.ToString();
                            break;

                        case "TerminalId":
                            item.ShortValue = txtTerminalId.Text.ToString();
                            break;

                        case "zarinpalmerchant":
                            item.ShortValue = txtZarin.Text;
                            break;

                        default:
                            break;
                    }

                    _data.SubmitChanges();
                }
            }
            SystemConfig.ClearInstance();
            Response.Redirect("setting.aspx#tabs-1");
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button11_Click(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (Product item in _data.Products)
                {
                    foreach (string keyitem in item.Keywords.Split(','))
                    {
                        try
                        {
                            if (keyitem.Length <= 2) continue;
                            if (_data.ProductKeywords.Any(a => a.KeyName == keyitem.ValidPersian()))
                            {
                                ProductKeyword tmpkey = _data.ProductKeywords.SingleOrDefault(a => a.KeyName == keyitem.ValidPersian());

                                ++tmpkey.Count;
                                _data.SubmitChanges();

                            }
                            else
                            {

                                ProductKeyword tmpkey = new ProductKeyword();
                                tmpkey.KeyName = keyitem.ValidPersian();

                                tmpkey.Count = 1;
                                _data.ProductKeywords.InsertOnSubmit(tmpkey);
                                _data.SubmitChanges();
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            using (DataAccessDataContext _data = new DataAccessDataContext())
            {
                foreach (settingValue item in _data.settingValues.Where(a => a.Short == "SMSSignature" ||
                 a.Short == "SMSToSeller" || a.Short == "SMSNumber" || a.Short == "ManagerMob" || a.Short == "SMSToBuyer"))
                {
                    switch (item.Short)
                    {
                        case "SMSSignature":
                            item.ShortValue = txtsign.Text;
                            break;
                            
                        case "SMSNumber":
                            item.ShortValue = txtsmsnumber.Text;
                            break;

                        case "ManagerMob":
                            item.ShortValue = txtsmsManagermob.Text;
                            break;

                        case "SMSToBuyer":
                            item.ShortValue = txtsmstext.Text;
                            break;
                        default:
                            break;
                    }
                    _data.SubmitChanges();
                }
            }
            SystemConfig.ClearInstance();
            Response.Redirect("setting.aspx#tabs-5");
        }

        
    }
}