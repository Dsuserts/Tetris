using System.Media;
using WMPLib;

public  class SoundControl
{
    private  readonly WMPLib.WindowsMediaPlayer mainSound;
    private  SoundPlayer sound;
    private bool e = true;

    private bool enableSound
    {
        get { return e; }
        set
        {
            if (value)
            {
                e = value;
                mainSound.controls.play();
            }
            else
            {
                e = value;
                mainSound.controls.pause();
            }
        }

    }

    public SoundControl()
    {
        sound = new SoundPlayer();
        mainSound = new WMPLib.WindowsMediaPlayer();
        mainSound.URL = "1.mp3";
        mainSound.settings.setMode("loop", true);
        sound.SoundLocation = "2.wav";

    }

    public void playMain()
    {
        if (enableSound)
            mainSound.controls.play();
    }

    public void playSuccess()
    {
        if (enableSound)
            sound.Play();
    }

    public bool mute()
    {
        e = !e;
        enableSound = e;
        return e;
    }


}
