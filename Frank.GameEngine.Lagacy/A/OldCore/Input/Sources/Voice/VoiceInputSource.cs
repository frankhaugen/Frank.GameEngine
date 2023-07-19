namespace Frank.GameEngine.Lagacy.A.OldCore.Input.Sources.Voice;

// public class VoiceInputSource : InputSource
// {
//     private readonly SpeechRecognitionEngine _speechRecognitionEngine;
//     private readonly VoiceCommandDictionary _voiceCommands;
//
//     public VoiceInputSource()
//     {
//         _voiceCommands = new VoiceCommandDictionary();
//         _speechRecognitionEngine = new SpeechRecognitionEngine();
//
//         foreach (var (command, grammar) in _voiceCommands)
//         {
//             _speechRecognitionEngine.LoadGrammar(grammar);
//         }
//
//         _speechRecognitionEngine.SpeechRecognized += (sender, e) =>
//         {
//             if (Enum.TryParse<VoiceCommand>(e.Result.Grammar.RuleName, out var commandName) && _voiceCommands.TryGetValue(commandName, out var command))
//             {
//                 VoiceCommandRecognized?.Invoke(this, new VoiceCommandRecognizedEventArgs(commandName, e.Result.Confidence));
//             }
//         };
//     }
//
//     public override void Update(GameTime gameTime)
//     {
//     }
//     public event EventHandler<VoiceCommandRecognizedEventArgs>? VoiceCommandRecognized;
// }