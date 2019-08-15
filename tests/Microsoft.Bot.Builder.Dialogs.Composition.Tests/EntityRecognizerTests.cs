﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.Dialogs.Composition.Recognizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.Dialogs.Composition.Recognizers.Tests
{
    [TestClass]
    public class EntityRecognizerTests
    {
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };

        private static Lazy<EntityRecognizerSet> recognizers = new Lazy<EntityRecognizerSet>(() =>
        {
            EntityRecognizerSet set = new EntityRecognizerSet();
            set.Recognizers.Add(new AgeEntityRecognizer());
            set.Recognizers.Add(new ChoiceEntityRecognizer());
            set.Recognizers.Add(new CurrencyEntityRecognizer());
            set.Recognizers.Add(new DateTimeEntityRecognizer());
            set.Recognizers.Add(new DimensionEntityRecognizer());
            set.Recognizers.Add(new EmailEntityRecognizer());
            set.Recognizers.Add(new GuidEntityRecognizer());
            set.Recognizers.Add(new HashtagEntityRecognizer());
            set.Recognizers.Add(new IpEntityRecognizer());
            set.Recognizers.Add(new MentionEntityRecognizer());
            set.Recognizers.Add(new NumberEntityRecognizer());
            set.Recognizers.Add(new NumberRangeEntityRecognizer());
            set.Recognizers.Add(new OrdinalEntityRecognizer());
            set.Recognizers.Add(new PercentageEntityRecognizer());
            set.Recognizers.Add(new PhoneNumberEntityRecognizer());
            set.Recognizers.Add(new TemperatureEntityRecognizer());
            set.Recognizers.Add(new UrlEntityRecognizer());
            return set;
        });

        private TurnContext GetTurnContext(string text, string locale = "en-us")
        {
            return new TurnContext(new TestAdapter(TestAdapter.CreateConversation(TestContext.TestName)), new Schema.Activity(type: Schema.ActivityTypes.Message, text: text, locale: locale));
        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestAge()
        {
            var turnContext = GetTurnContext("This is a test of one, 2, three years old");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(6, results.Count, "Should be 5 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "age").Count(), "Should have 1 age entity");
        }

        [TestMethod]
        public void TestChoice()
        {
            var turnContext = GetTurnContext("yes, please");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(2, results.Count, "Should be 1 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "boolean").Count(), "Should have 1 age results");
        }

        [TestMethod]
        public void TestCurrency()
        {
            var turnContext = GetTurnContext("I would pay four dollars for that.");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(3, results.Count, "Should be 2 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "currency").Count(), "Should have 1 currency result");
        }

        [TestMethod]
        public void TestDateTime()
        {
            var turnContext = GetTurnContext("Next thursday at 4pm.");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(3, results.Count, "Should be 2 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "datetimeV2.datetime").Count(), "Should have 1 datetime result");
        }

        [TestMethod]
        public void TestDimension()
        {
            var turnContext = GetTurnContext("I think he's 5 foot ten");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(4, results.Count, "Should be 3 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "dimension").Count(), "Should have 1 dimension result");
        }

        [TestMethod]
        public void TestEmail()
        {
            var turnContext = GetTurnContext("my email address is foo@att.uk.co");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(2, results.Count, "Should be 1 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "email").Count(), "Should have 1 email result");
        }

        [TestMethod]
        public void TestGuid()
        {
            var guid = Guid.Empty;
            var turnContext = GetTurnContext($"my account number is {guid}...");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(7, results.Count, "Should be 6 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "guid").Count(), "Should have 1 guid result");
        }

        [TestMethod]
        public void TestHashtag()
        {
            var turnContext = GetTurnContext($"I'm so cool #cool #groovy...");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(3, results.Count, "Should be 2 entities found");
            Assert.AreEqual(2, results.Where(entity => entity.Type == "hashtag").Count(), "Should have 2 hashtag result");
        }

        [TestMethod]
        public void TestIp()
        {
            var turnContext = GetTurnContext($"My address is 1.2.3.4");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(6, results.Count, "Should be 5 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "ip").Count(), "Should have 1 ip result");
        }

        [TestMethod]
        public void TestMention()
        {
            var turnContext = GetTurnContext($"Tell @joesmith I'm coming...");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(2, results.Count, "Should be 1 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "mention").Count(), "Should have 1 mention result");
        }

        [TestMethod]
        public void TestNumber()
        {
            var turnContext = GetTurnContext("This is a test of one, 2, three");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(4, results.Count, "Should be 3 numbers found");
            Assert.AreEqual(3, results.Where(entity => entity.Type == "number").Count(), "Should have 3 numbers");
        }

        [TestMethod]
        public void TestNumberRange()
        {
            var turnContext = GetTurnContext("there are 3 to 5 of them");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(4, results.Count, "Should be 3 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "numberrange").Count(), "Should have 1 number range");
        }

        [TestMethod]
        public void TestOrdinal()
        {
            var turnContext = GetTurnContext("First, second or third");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(4, results.Count, "Should be 3 entities found");
            Assert.AreEqual(3, results.Where(entity => entity.Type == "ordinal").Count(), "Should have 3 ordinals");
        }

        [TestMethod]
        public void TestPercentage()
        {
            var turnContext = GetTurnContext("The population hit 33.3%");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(3, results.Count, "Should be 2 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "percentage").Count(), "Should have 1 percentage");
        }

        [TestMethod]
        public void TestPhoneNumber()
        {
            var turnContext = GetTurnContext("Call 425-882-8080");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(5, results.Count, "Should be 4 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "phonenumber").Count(), "Should have 1 phonenumber");
        }

        [TestMethod]
        public void TestTemperature()
        {
            var turnContext = GetTurnContext("set the oven to 350 degrees");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(3, results.Count, "Should be 2 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "temperature").Count(), "Should have 1 temperature");
        }

        [TestMethod]
        public void TestUrl()
        {
            var turnContext = GetTurnContext("go to http://about.me for more info");
            var results = recognizers.Value.RecognizeEntities(turnContext).Result;

            Assert.AreEqual(2, results.Count, "Should be 1 entities found");
            Assert.AreEqual(1, results.Where(entity => entity.Type == "url").Count(), "Should have 1 url");
        }
    }
}
