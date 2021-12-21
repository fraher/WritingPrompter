using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WritingPrompter.Shared;

namespace WritingPrompter.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WritingPromptController : ControllerBase
    {
        private readonly ILogger<WritingPromptController> _logger;

        public WritingPromptController(ILogger<WritingPromptController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WritingPrompt> Get()
        {

            var fs = new FileStream(@"./Content/prompts.json",
                                  FileMode.Open);


            using StreamReader reader = new StreamReader(fs);
            
            string result = reader.ReadToEnd();

            Dictionary<string, List<string>> prompts = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(result);

            List<WritingPrompt> writingPrompts = new();

            foreach (KeyValuePair<string, List<string>> genere in prompts)
            {
                foreach (string prompt in genere.Value)
                {
                    writingPrompts.Add(new WritingPrompt
                    {
                        Genre = genere.Key,
                        Prompt = prompt
                    });
                }
            }

            return writingPrompts;
        }
    }
}
