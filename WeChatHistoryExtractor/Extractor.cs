using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace WeChatHistoryExtractor
{
    internal class Extractor
    {
        private readonly MacDriver _driver;

        public Extractor(MacDriver driver)
        {
            _driver = driver;
        }

        const string HistoryWindowPathFormat = "/AXApplication[@AXTitle='WeChat']/AXWindow[{0}]";
        

        
        public List<ChatMessage> ExtractMessage()
        {   
            var winIndex = FindHistoryWindowIndex();
            if (winIndex < 0)
            {
                throw new InvalidOperationException("There is no open WeChat chat history window.");
            }
            
            var winBasePath = string.Format(HistoryWindowPathFormat, winIndex);

            return ExtractMessages(winBasePath);
        }



        List<ChatMessage> ExtractMessages(string winBasePath)
        {
            const string fieldFormat = "/AXScrollArea[0]/AXTable[0]/AXRow[{0}]/AXCell[0]/AXTextArea[{1}]";
            
            var row = 1;
            var allMessages = new List<ChatMessage>();
            do
            {
                var authorFieldPath = string.Format(fieldFormat, row, 0);
                var authorField = FindElementOnPage(string.Concat(winBasePath, authorFieldPath));
                if (authorField == null)
                {
                    break;
                }

                var timeField = FindElementOnPage(string.Concat(winBasePath,  string.Format(fieldFormat, row, 1)));
                var contentField = FindElementOnPage(string.Concat(winBasePath, string.Format(fieldFormat, row, 2)));
                var message = new ChatMessage
                {
                    author = authorField.Text,
                    content = contentField?.Text,
                    time = timeField.Text
                };
                allMessages.Add(message);
                row++;
            } while (true);

            return allMessages;
        }
        
        int FindHistoryWindowIndex()
        {
            var titles = new[] {"Transcript", "Chat History", "聊天记录"};
            var textareaPathSuffix = "/AXTextArea";
            for (var i = 0; i < 5; i++)
            {
                var winXPath = string.Format(HistoryWindowPathFormat, i);
                var win = FindElementOnPage(winXPath);
                if (win == null)
                {
                    continue;
                }

                var textareaPath = string.Concat(winXPath, textareaPathSuffix);
                var textarea = FindElementOnPage(textareaPath);
                var text = textarea?.Text;
                
                if (text != null && titles.Any(t => text.Contains(t)))
                {
                    return i;
                }
            }

            return -1;
        }


        RemoteWebElement FindElementOnPage(string xpath)
        {
            try
            {
                return _driver.FindElementByXPath(xpath);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }



    }
}