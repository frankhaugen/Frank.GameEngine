namespace Frank.GameEngine.Core.Input.Sources.Voice;

public struct VoiceCommandRecognizedEventArgs
{
    public VoiceCommand Command { get; }
    public float Confidence { get; }

    public VoiceCommandRecognizedEventArgs(VoiceCommand command, float confidence)
    {
        Command = command;
        Confidence = confidence;
    }
}