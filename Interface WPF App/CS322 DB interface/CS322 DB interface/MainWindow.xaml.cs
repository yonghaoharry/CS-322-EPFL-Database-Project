using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Proj
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] table_names { get; set; }

        public string[] table_queries { get; set; }
        public string[] table_regionChoice { get; set; }

        public MySqlConnection airbnbConnection;

        List<String> Verification = new List<String>();
        public MainWindow()
        {
            InitializeComponent();

            table_names = new string[] { "Listing", "Host", "Country", "Score" };

            table_queries = new string[] { "Select cheapest listing on certain date", "Average price of house with certain number of certain rooms" };
            table_regionChoice = new string[] { "El Raval", "El Poblenou", "L'Antiga Esquerra de l'Eixample", "El Born" };
            DataContext = this;
            DatabaseConnect();
        }



        private void Bttn_srch_Click(object sender, EventArgs e)
        {

            String textB_srch_value = TextB_srch.Text;
            // ..........................................
        }

        private void Bttn_dlet_Click(object sender, EventArgs e)
        {

            String textB_dlet_value = TextB_dlet.Text;
            // ..........................................
        }

        private void QuerySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (selectQuery.SelectedIndex) {
                case 1:
                    newQuerySelection(sender, e);
                    firstQuery.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void newQuerySelection(object sender, SelectionChangedEventArgs e)
        {
            firstQuery.Visibility = Visibility.Collapsed;
            //InsertHost.Visibility = Visibility.Collapsed;
            //InsertCountry.Visibility = Visibility.Collapsed;
        }

        private void InsertTableOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (InsertTableOption.SelectedItem.ToString())
            {
                case "Listing":
                    newSelection(sender, e);
                    InsertListing.Visibility = Visibility.Visible;
                    break;
                case "Host":
                    newSelection(sender, e);
                    InsertHost.Visibility = Visibility.Visible;
                    break;
                case "Country":
                    newSelection(sender, e);
                    InsertCountry.Visibility = Visibility.Visible;
                    break;
                case "Score":
                    newSelection(sender, e);
                    InsertScore.Visibility = Visibility.Visible;
                    break;

            }


        }
        private void newSelection(object sender, SelectionChangedEventArgs e)
        {
            InsertListing.Visibility = Visibility.Collapsed;
            InsertHost.Visibility = Visibility.Collapsed;
            InsertCountry.Visibility = Visibility.Collapsed;
            InsertScore.Visibility = Visibility.Collapsed;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckedHandle(sender as CheckBox);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UncheckedHandle(sender as CheckBox);
        }

        void CheckedHandle(CheckBox checkBox)
        {
            //// Use IsChecked.
            //bool flag = checkBox.IsChecked.Value;

            //// Assign Window Title.
            //this.Title = "IsChecked = " + flag.ToString();

            Verification.Add(checkBox.Content.ToString());

        }
        void UncheckedHandle(CheckBox checkBox)
        {
            //// Use IsChecked.
            //bool flag = checkBox.IsChecked.Value;

            //// Assign Window Title.
            //this.Title = "IsChecked = " + flag.ToString();

            Verification.Remove(checkBox.Content.ToString());

        }



        private void DatabaseConnect()
        {
            string connectionstring = "SERVER=localhost;DATABASE=airbnb;UID=root;PASSWORD=yh19981118";
            airbnbConnection = new MySqlConnection(connectionstring);
            airbnbConnection.Open();
        }

        private void DatabaseClose()
        {
            airbnbConnection.Close();
        }

        public void InsertTable(object sender, RoutedEventArgs e)
        {
            string tableName;
            tableName = InsertTableOption.Text;
            switch (tableName)
            {
                case "Listing":
                    string listingID = listingIDInput.Text;
                    string listingURL = listingUrlInput.Text;
                    string listingName = listingNameInput.Text;
                    string summary = summaryInput.Text;
                    string space = spaceInput.Text;
                    string description = descriptionInput.Text;
                    string neighborhoodOverview = neighDesInput.Text;
                    string notes = notesInput.Text;
                    string transitInfo = transitInput.Text;
                    string accessInfo = accessInput.Text;
                    string interactionInfo = interactionInput.Text;
                    string houseRule = ruleInput.Text;
                    string pictureURL = picInput.Text;
                    string hostID = listingHostIDInput.Text;
                    string neighborhood = neighRegionInput.Text;
                    string latitude = latitudeInput.Text;
                    string longitude = longitudeInput.Text;
                    string minStay = minStayInput.Text;
                    string maxStay = maxStayInput.Text;

                    string insertListingCommandString = "INSERT INTO Listing(listing_id, listing_url, listing_name, summary, space, " +
                        "listing_description, neighborhood_overview, notes, transit, access, interaction, house_rules, picture_url, " +
                        "host_id, neighborhood, latitude, longitude, minimum_nights, maximum_nights)" +
                        "Values(" + listingID + ", " +
                        "'" + listingURL + "', " +
                        "'" + listingName + "', " +
                        "'" + summary + "', " +
                        "'" + space + "', " +
                        "'" + description + "', " +
                        "'" + neighborhoodOverview + "', " +
                        "'" + notes + "', " +
                        "'" + transitInfo + "', " +
                        "'" + accessInfo + "', " +
                        "'" + interactionInfo + "', " +
                        "'" + houseRule + "', " +
                        "'" + pictureURL + "', " +
                        hostID + ", " +
                        "'" + neighborhood + "', " +
                        latitude + ", " +
                        longitude + ", " +
                        minStay + ", " +
                        maxStay + ")";

                    MySqlCommand insertCommand = new MySqlCommand(insertListingCommandString, airbnbConnection);
                    int rows = insertCommand.ExecuteNonQuery();
                    MessageBox.Show("success" + " number of rows affected: " + rows.ToString());
                    break;
                case "Host":
                    string hostHostID = hostHostIDInput.Text;
                    string hostURL = hostUrlInput.Text;
                    string hostName = hostNameInput.Text;
                    string hostSince = sinceInput.Text;
                    string hostAbout = aboutInput.Text;
                    string responseTime = responseTimeInput.Text;
                    string responseRate = responseRateInput.Text;
                    string hostThumbnailURL = thumbnailInput.Text;
                    string hostPictureURL = hostPicInput.Text;
                    string hostNeighborhood = hostNeighReigionInput.Text;

                    List<CheckBox> verificationCheckBox = new List<CheckBox>();
                    verificationCheckBox.Add(Email);
                    verificationCheckBox.Add(Phone);
                    verificationCheckBox.Add(Reviews);
                    verificationCheckBox.Add(Jumio);
                    verificationCheckBox.Add(Offline_gov_id);
                    verificationCheckBox.Add(Gov_id);
                    verificationCheckBox.Add(Facebook);
                    verificationCheckBox.Add(EmManual_offline);
                    verificationCheckBox.Add(Work_email);
                    verificationCheckBox.Add(Selfie);
                    verificationCheckBox.Add(Identity_manual);
                    string verificationCheck = "[";
                    foreach (CheckBox box in verificationCheckBox)
                    {
                        if (box.IsChecked == true)
                        {
                            verificationCheck += "'" + box.Content.ToString() + "', ";
                        }
                    }
                    verificationCheck += "]";



                    string insertHostCommandString = "INSERT INTO Host_table(host_id, host_url, host_name, host_since, host_about, " +
                        "host_response_time, host_response_rate, host_thumbnail_url, host_picture_url, host_neighborhood, host_verifications)" +
                        "Values(" + hostHostID + ", " +
                        "'" + hostURL + "', " +
                        "'" + hostName + "', " +
                        "'" + hostSince + "', " +
                        "'" + hostAbout + "', " +
                        "'" + responseTime + "', " +
                        responseRate + ", " +
                        "'" + hostThumbnailURL + "', " +
                        "'" + hostPictureURL + "', " +
                        hostNeighborhood + ", " +
                        "'" + verificationCheck + "')";
                    MySqlCommand InsertHostCommand = new MySqlCommand(insertHostCommandString, airbnbConnection);
                    rows = InsertHostCommand.ExecuteNonQuery();
                    MessageBox.Show("success" + " number of rows affected: " + rows.ToString());
                    break;
                case "Country":
                    string getMaxCountryString = "SELECT MAX(country_id) from country";
                    MySqlCommand getMaxCountryCommand = new MySqlCommand(getMaxCountryString, airbnbConnection);
                    string maxCountry = getMaxCountryCommand.ExecuteScalar().ToString();
                    string targetCountryNo = (System.Convert.ToInt32(maxCountry) + 1).ToString();
                    string countryName = countryInput.Text;

                    string insertCountryCommandString = "INSERT INTO country(country_id, country_name) " +
                        "VALUES(" + targetCountryNo + ", " + "'" + countryName + "'" + ")";
                    MySqlCommand insertCountryCommand = new MySqlCommand(insertCountryCommandString, airbnbConnection);
                    rows = insertCountryCommand.ExecuteNonQuery();
                    MessageBox.Show("success" + " number of rows affected: " + rows.ToString());
                    break;
                case "Score":
                    string scoreListingID = ScoreListingID.Text;
                    string ratingScore = RatingScore.Text;
                    string accuracyScore = AccuracyScore.Text;
                    string cleanlinessScore = CleanlinessScore.Text;
                    string checkInScore = CheckInScore.Text;
                    string commScore = CommScore.Text;
                    string locScore = LocScore.Text;
                    string valueScore = ValScore.Text;
                    string insertScoreCommandString = "INSERT INTO score(listing_id, review_scores_rating, review_scores_accuracy, review_scores_clean, " +
                        "review_scores_checkin, review_scores_communication, review_scores_location, review_scores_value)" +
                        "VALUES(" +
                        scoreListingID + ", " +
                        ratingScore + ", " +
                        accuracyScore + ", " +
                        cleanlinessScore + ", " +
                        checkInScore + ", " +
                        commScore + ", " +
                        locScore + ", " +
                        valueScore +
                        ")";
                    MySqlCommand insertScoreCommand = new MySqlCommand(insertScoreCommandString, airbnbConnection);
                    rows = insertScoreCommand.ExecuteNonQuery();
                    MessageBox.Show("success" + " number of rows affected: " + rows.ToString());
                    break;
                default:
                    break;
            }
        }

        private void AUD(string sql_stmt, string mode)
        {
            MySqlCommand cmd = new MySqlCommand(sql_stmt, airbnbConnection);
            MySqlDataReader rd = cmd.ExecuteReader();
            switch (mode)
            {
                case "Listing":

                    break;
                case "Host":
                    break;
                default:
                    break;
            }
        }

        private void BttnSrchClick(object sender, RoutedEventArgs e)
        {

            string tableName = ComboB_srch.Text;
            string textB_srch_value = TextB_srch.Text;

            switch (tableName)
            {
                case "Listing":
                    string sql = "SELECT * FROM Listing L WHERE L.listing_id LIKE '%" + textB_srch_value + "%' OR "
                                + "L.listing_url LIKE '%" + textB_srch_value + "%' OR "
                                + "L.listing_name LIKE '%" + textB_srch_value + "%' OR "
                                + "L.summary LIKE '%" + textB_srch_value + "%' OR "
                                + "L.space LIKE '%" + textB_srch_value + "%' OR "
                                + "L.listing_description LIKE '%" + textB_srch_value + "%' OR "
                                + "L.neighborhood_overview LIKE '%" + textB_srch_value + "%' OR "
                                + "L.notes LIKE '%" + textB_srch_value + "%' OR "
                                + "L.transit LIKE '%" + textB_srch_value + "%' OR "
                                + "L.access LIKE '%" + textB_srch_value + "%' OR "
                                + "L.interaction LIKE '%" + textB_srch_value + "%' OR "
                                + "L.house_rules LIKE '%" + textB_srch_value + "%' OR "
                                + "L.picture_url LIKE '%" + textB_srch_value + "%' OR "
                                + "L.host_id LIKE '%" + textB_srch_value + "%' OR "
                                + "L.neighborhood LIKE '%" + textB_srch_value + "%' OR "
                                + "L.latitude LIKE '%" + textB_srch_value + "%' OR "
                                + "L.longitude LIKE '%" + textB_srch_value + "%' OR "
                                + "L.minimum_nights LIKE '%" + textB_srch_value + "%' OR "
                                + "L.maximum_nights LIKE '%" + textB_srch_value + "%'";
                    this.AUD(sql, "Listing");
                    break;
                case "Host":
                    sql = "SELECT * FROM Host_table H WHERE H.host_id LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_url LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_name LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_since LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_about LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_response_time LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_response_rate LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_thumbnail_url LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_picture_url LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_neighborhood LIKE '%" + textB_srch_value + "%' OR "
                                        + "H.host_verifications LIKE '%" + textB_srch_value + "%'";
                    this.AUD(sql, "Host");
                    break;
                default:
                    break;

            }


        }

        private void Bttn_dlet_Click(object sender, RoutedEventArgs e)
        {

            string tableName = ComboB_dlet.Text;
            string textB_dlet_value = TextB_dlet.Text;

            switch (tableName)
            {
                case "Listing":
                    string sql = "DELETE FROM Listing WHERE Listing.listing_id = " + textB_dlet_value;
                    MySqlCommand cmd = new MySqlCommand(sql, airbnbConnection);
                    cmd.ExecuteNonQuery();
                    break;
                case "Host":
                    sql = "DELETE FROM Host_table WHERE Host_table.host_id = " + textB_dlet_value;
                    cmd = new MySqlCommand(sql, airbnbConnection);
                    MessageBox.Show(sql);
                    cmd.ExecuteNonQuery();
                    break;
                case "Country":
                    sql = "DELETE FROM Country WHERE Country.country_id = " + textB_dlet_value;
                    cmd = new MySqlCommand(sql, airbnbConnection);
                    cmd.ExecuteNonQuery();
                    break;
                case "Score":
                    sql = "DELETE FROM Score WHERE Score.score_id = " + textB_dlet_value;
                    cmd = new MySqlCommand(sql, airbnbConnection);
                    cmd.ExecuteNonQuery();
                    break;
            }

        }

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }

    public class Listing{
        protected int listing_id;
        protected string listing_url;
        protected string listing_name;
        protected string summary;
        protected string space;
        protected string listing_description;
        protected string neighborhood_overview;
        protected string notes;
        protected string transit;
        protected string access;
        protected string interaction;
        protected string house_rules;
        protected string picture_url;
        protected int host_id;
        protected string neighborhood;
        protected double latitude;
        protected double longitude;
        protected int minimum_nights;
        protected int maximum_nights;
        
        public Listing(int listing_id,
        string listing_url,string listing_name,string summary,string space,string listing_description,string neighborhood_overview,
        string notes,string transit,string access,string interaction,string house_rules,string picture_url,int host_id,string neighborhood,
        double latitude,double longitude, int minimum_nights, int maximum_nights)
        {
            this.listing_id = listing_id;
            this.listing_url = listing_url;
            this.listing_name = listing_name;
            this.summary = summary;
            this.space = space;
            this.listing_description = listing_description;
            this.neighborhood_overview = neighborhood_overview;
            this.notes = notes;
            this.transit = transit;
            this.access = access;
            this.interaction = interaction;
            this.house_rules = house_rules;
            this.picture_url = picture_url;
            this.host_id = host_id;
            this.neighborhood = neighborhood;
            this.latitude = latitude;
            this.longitude = longitude;
            this.minimum_nights = minimum_nights;
            this.maximum_nights = maximum_nights;
        }
    }

    public class Host
    {
        
    }


}

