using DotFuzzy;
using System;
using System.Windows.Forms;

namespace Sinoy_FuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fuzzyEngine;
        MembershipFunctionCollection milliliter, temperature, watts;
        LinguisticVariable mymillimeter, mytemperature, mywatts;
        FuzzyRuleCollection myrules;

        public Form1()
        {
            InitializeComponent();
            SetMembers();
            SetRules();
            SetFuzzyEngine();
        }

        public void SetMembers()
        {

            temperature = new MembershipFunctionCollection();
            temperature.Add(new MembershipFunction("Cold", 1, 1, 20, 40));
            temperature.Add(new MembershipFunction("Warm", 30, 40, 50, 60));
            temperature.Add(new MembershipFunction("Hot", 50, 70, 100, 100));
            mytemperature = new LinguisticVariable("Temperature", temperature);

            milliliter = new MembershipFunctionCollection();
            milliliter.Add(new MembershipFunction("Low", 1, 1, 200, 400));
            milliliter.Add(new MembershipFunction("Medium", 300, 400, 500, 600));
            milliliter.Add(new MembershipFunction("High", 500, 700, 1000, 1000));
            mymillimeter = new LinguisticVariable("Milliliter", milliliter);

            watts = new MembershipFunctionCollection();
            watts.Add(new MembershipFunction("VeryLow", 1, 1, 1000, 1500));
            watts.Add(new MembershipFunction("Low", 1250, 1500, 2500, 3000));
            watts.Add(new MembershipFunction("Medium", 2750, 3000, 4000, 4500));
            watts.Add(new MembershipFunction("High", 4250, 4500, 5500, 6000));
            watts.Add(new MembershipFunction("VeryHigh", 5750, 6000, 7000, 7500));
            mywatts = new LinguisticVariable("Watts", watts);

        }

        public void SetRules()
        {
            myrules = new FuzzyRuleCollection();
            myrules.Add(new FuzzyRule("IF (Temperature IS Hot) AND (Milliliter IS Low) THEN Watts IS VeryHigh"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Hot) AND (Milliliter IS Medium) THEN Watts IS VeryHigh"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Hot) AND (Milliliter IS High) THEN Watts IS VeryHigh"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Warm) AND (Milliliter IS Low) THEN Watts IS VeryLow"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Warm) AND (Milliliter IS Medium) THEN Watts IS Medium"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Warm) AND (Milliliter IS High) THEN Watts IS High"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Cold) AND (Milliliter IS Low) THEN Watts IS VeryLow"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Cold) AND (Milliliter IS Medium) THEN Watts IS Low"));
            myrules.Add(new FuzzyRule("IF (Temperature IS Cold) AND (Milliliter IS High) THEN Watts IS Medium"));
        }

        public void SetFuzzyEngine()
        {
            fuzzyEngine = new FuzzyEngine();
            fuzzyEngine.LinguisticVariableCollection.Add(mytemperature);
            fuzzyEngine.LinguisticVariableCollection.Add(mymillimeter);
            fuzzyEngine.LinguisticVariableCollection.Add(mywatts);
            fuzzyEngine.FuzzyRuleCollection = myrules;
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            mymillimeter.InputValue = (Convert.ToDouble(textBox1.Text));
            mymillimeter.Fuzzify("Medium");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetFuzzyEngine();
            fuzzyEngine.Consequent = "Watts";
            textBox3.Text = fuzzyEngine.Defuzzify() + "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ComputeNewTemperature();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FuziffyValues();
            Defuzzy();
            ComputeNewTemperature();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mytemperature.InputValue = (Convert.ToDouble(textBox2.Text));
            mytemperature.Fuzzify("Warm");
        }

        public void FuziffyValues()
        {
            mymillimeter.InputValue = (Convert.ToDouble(textBox1.Text));
            mymillimeter.Fuzzify("Low");
            mytemperature.InputValue = (Convert.ToDouble(textBox2.Text));
            mytemperature.Fuzzify("Cold");
        }

        public void Defuzzy()
        {
            SetFuzzyEngine();
            fuzzyEngine.Consequent = "Watts";
            textBox3.Text = "" + fuzzyEngine.Defuzzify();
        }
        public void ComputeNewTemperature()
        {

            double oldtemperature = Convert.ToDouble(textBox2.Text);
            double oldwatts = Convert.ToDouble(textBox3.Text);
            double oldmilliliter = Convert.ToDouble(textBox1.Text);
            double newtemperature = ((1 - 0.1) * (oldtemperature)) + (oldwatts - (0.1 * oldmilliliter));
            textBox2.Text = "" + newtemperature;
        }

    }
}
