using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViewModel = SCMCore.ViewModel;
using Bis = SCMCore.DatabaseLayer;
using SCMCore.ExtensionMethod;
using SCMCore.Classes;
using System.IO;
using AjaxControlToolkit;

namespace SCMCore.Admin.UserControl
{
    public partial class CasscadDropDownCountry : System.Web.UI.UserControl
    {
        Bis.CountryMethod bisCountry = new Bis.CountryMethod();
        Bis.StateMethod bisState = new Bis.StateMethod();
        Bis.CityMethod bisCity = new Bis.CityMethod();
    
        public string IDCity
        {
            get
            {
                return drpCity.SelectedValue;
            }
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
             
            }
        }
        public void fillCountry()
        {
            ViewModel.Search searchCountry = new ViewModel.Search();
            searchCountry.Order = "Order by Name_Fa";
            DataSet dsCountry = bisCountry.GetCountryData(searchCountry);
            drpCountry.Items.Clear();
            drpCountry.DataSource = dsCountry;
            drpCountry.DataTextField = "Name_Fa";
            drpCountry.DataValueField = "IDCountry";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem("-انتخاب کنید -", Guid.Empty.ToString()));
        }
        public void fillState(string IDCountry)
        {
            ViewModel.Search searchState = new ViewModel.Search();
            searchState.Filter = " and tblState.IDCountry= '" + IDCountry + "'";
            searchState.Order = "Order by Name_Fa";
            DataSet dsState = bisState.GetStateData(searchState);
            drpState.Items.Clear();
            drpState.DataSource = dsState;
            drpState.DataTextField = "Name_Fa";
            drpState.DataValueField = "IDState";
            drpState.DataBind();

        }
        public void fillCity(string IDState)
        {
            ViewModel.Search searchCity = new ViewModel.Search();
            searchCity.Filter = " and tblCity.IDState= '" + IDState + "'";
            searchCity.Order = "Order by Name_Fa";
            DataSet dsCity = bisCity.GetCityData(searchCity);
            drpCity.Items.Clear();
            drpCity.DataSource = dsCity;
            drpCity.DataTextField = "Name_Fa";
            drpCity.DataValueField = "IDCity";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("-انتخاب کنید -", Guid.Empty.ToString()));

        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpCountry.SelectedValue==Guid.Empty.ToString())
            {
                CleanDropDowns();
            }
            else
            {
                fillState(drpCountry.SelectedValue);
                drpState.Enabled = true;
            }
            
        }
        protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCity(drpState.SelectedValue);
            drpCity.Enabled = true;
        }
      

        public void CleanDropDowns()
        {
            drpCountry.SelectedIndex = 0;
            drpState.Items.Clear();
            drpState.Enabled = false;
            drpCity.Items.Clear();
            drpCity.Enabled = false;
        }
        public void SetDropDownsFromEdit(string IDCity)
        {
            if (IDCity != Guid.Empty.ToString())
            {
                drpCity.Enabled = true;
                drpState.Enabled = true;
             
                //city
                ViewModel.Search searchCity = new ViewModel.Search();
                searchCity.Filter = " and tblCity.IDCity= '" + IDCity + "'";
                DataSet dsCity = bisCity.GetCityData(searchCity);
                fillCity(dsCity.ReturnDataSetField("IDState"));
                drpCity.SelectedValue = dsCity.ReturnDataSetField("IDCity");
                //state
                ViewModel.Search searchState = new ViewModel.Search();
                searchState.Filter = " and tblState.IDState= '" + dsCity.ReturnDataSetField("IDState") + "'";
                DataSet dsState = bisState.GetStateData(searchState);
                fillState(dsState.ReturnDataSetField("IDCountry"));
                drpState.SelectedValue = dsState.ReturnDataSetField("IDState");
                //country
                drpCountry.SelectedValue = dsState.ReturnDataSetField("IDCountry");

            }
            else
            {
                CleanDropDowns();
            }


        }

 


    }
}