namespace Frank.GameEngine.Audio.Midi;

public static class SongFactory
{
    public static MidiSong GetHappyBirthdaySong()
    {
        var song = new MidiSong
        {
            Name = "Happy Birthday",
            Composer = "Patty and Mildred J. Hill",
            Tracks = new List<MidiTrack>
            {
                new MidiTrack
                {
                    Channel = 1,
                    Instrument = MidiInstrument.AcousticGrandPiano,
                    Notes = new List<MidiNote>
                    {
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.E4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Half },
                        new MidiNote { Note = Note.E4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.G4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.F4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.E4, Duration = Duration.Half }
                    }
                },
                new MidiTrack
                {
                    Channel = 2,
                    Instrument = MidiInstrument.Flute,
                    Notes = new List<MidiNote>
                    {
                        new MidiNote { Note = Note.Rest, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.Rest, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.G4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.G4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.F4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.E4, Duration = Duration.Half },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.B3, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Half }
                    }
                },
                new MidiTrack
                {
                    Channel = 3,
                    Instrument = MidiInstrument.Violin,
                    Notes = new List<MidiNote>
                    {
                        new MidiNote { Note = Note.Rest, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.Rest, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.Rest, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.E4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.B3, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Half },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.E4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Half },
                        new MidiNote { Note = Note.E4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.D4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.C4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.G4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.F4, Duration = Duration.Quarter },
                        new MidiNote { Note = Note.E4, Duration = Duration.Half }
                    }
                }
            }
        };

        return song;
    }

    public static MidiSong GetMacGyverTheme()
    {
        var song = new MidiSong
        {
            Name = "MacGyver Theme",
            Composer = "Randy Edelman",
            Tracks = new List<MidiTrack>
            {
                new MidiTrack
                {
                    Channel = 1,
                    Instrument = MidiInstrument.ElectricGuitarClean,
                    Notes = new List<MidiNote>
                    {
                        new MidiNote { Note = Note.G4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Half },
                        new MidiNote { Note = Note.F4, Duration = Duration.Half },
                        new MidiNote { Note = Note.E4, Duration = Duration.Half },
                        new MidiNote { Note = Note.F4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Half },
                        new MidiNote { Note = Note.G4, Duration = Duration.Half },
                        new MidiNote { Note = Note.F4, Duration = Duration.Half },
                        new MidiNote { Note = Note.E4, Duration = Duration.Half },
                        new MidiNote { Note = Note.F4, Duration = Duration.Half },
                    }
                },
                // ... Add other tracks for more instruments
            }
        };

        return song;
    }

    public static MidiSong CreateJingleBellsSong()
    {
        return new MidiSong()
        {
            Name = "Jingle Bells",
            Composer = "James Lord Pierpont",
            Tracks = new List<MidiTrack>
            {
                // Melody
                new MidiTrack
                {
                    Channel = 1,
                    Instrument = MidiInstrument.Violin,
                    Notes = new List<MidiNote>
                    {
                        new MidiNote {Note = Note.E4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.E4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.E4, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.E4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.E4, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.G4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.C4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.D4, Duration = Duration.Eighth},
                        new MidiNote {Note = Note.E4, Duration = Duration.Quarter},
                    }
                },
                // Bass line
                new MidiTrack
                {
                    Channel = 2,
                    Instrument = MidiInstrument.AcousticBass,
                    Notes = new List<MidiNote>
                    {
                        new MidiNote {Note = Note.E2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E2, Duration = Duration.Half},
                        new MidiNote {Note = Note.E2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E2, Duration = Duration.Half},
                        new MidiNote {Note = Note.E2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.G2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.C2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.D2, Duration = Duration.Quarter},
                        new MidiNote {Note = Note.E2, Duration = Duration.Half},
                    }
                }
            }
        };
    }

    // Add more methods here to create other songs...
}