using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �Ŵ���
public class SoundManager
{
    // �̸� ������ ũ���� ��������ҽ� ����
	AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];
    // 1ȸ �̻� ����� ����� ������ �����
	Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip> ();

    // �ʱ�ȭ
	public void Init()
	{
		GameObject root = GameObject.Find("Sound");
		if(root == null)
		{
            root = new GameObject { name = "Sound" };
			Object.DontDestroyOnLoad(root);

			string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
			for(int i = 0; i < soundNames.Length - 1; i++)
			{
				GameObject sound = new GameObject { name = soundNames[i] };
				_audioSource[i] = sound.AddComponent<AudioSource>();
				sound.transform.parent = root.transform;
			}
		}
        // Bgm�� �ݺ�����
		_audioSource[(int)Define.Sound.Bgm].loop = true;
		
        // ���� ����
        _audioSource[(int)Define.Sound.Bgm].volume = Managers.GData.player.BGMVol * 0.6f;
        _audioSource[(int)Define.Sound.Effect].volume = Managers.GData.player.EffectVol * 0.8f;
	}
	// �������� ����� ������ ����
	public void Clear()
	{
		foreach(var audio in _audioSource)
		{
			audio.clip = null;
			audio.Stop();
		}
		_audioClips.Clear();
	}

    // Resources������ ��ξȿ� �ִ� ���� ���
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        // ������ ������� �����Ŭ���� �����ϴ��� Ȯ�� �� ��������
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        // ������ ������ Bgm�̶�� 
        if (type == Define.Sound.Bgm)
        {
            // ���� ������� ���� ���߰�
            AudioSource audioSource = _audioSource[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            // ���� ���
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        // ������ ������ Effect���
        else
        {
            // �ٷ� ���(1ȸ)
            AudioSource audioSource = _audioSource[(int)Define.Sound.Effect];
            // ��ġ���� �����ϰ� �־� �ߺ��� ����Ʈ ����� �������� �̸� ����
            audioSource.pitch = Random.Range(pitch*0.9f, pitch);
            audioSource.PlayOneShot(audioClip);
        }
    }

    // ��η� �Է��� ��ġ�� �ִ� ����� Ŭ�� ã��
    // type���� ������ ������ҽ��� �־��
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            // Bgm�� ��� ������ ����Ǵ°� �ƴϱ⿡ �ʿ�� ���ҽ��������� ã�ƿ�
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            // Effect�� ��� ������ ����Ǳ⿡ 1ȸ �̻� ����� �����Ŭ���� �̸� ������ �ξ��ٰ� �����پ�
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                // ����� ���� ���ٸ� ���ҽ��������� ã�� ������
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }

    public void BgmStop()
    {
		// ���� ������� bgm�� �ִٸ� ����
		AudioSource audioSource = _audioSource[(int)Define.Sound.Bgm];
		if (audioSource.isPlaying)
			audioSource.Stop();
	}

	// Bgm�� Effect�� ���� ����
	public void SetBGMVol(float vol)
    {
        Managers.GData.player.BGMVol = vol;
        _audioSource[(int)Define.Sound.Bgm].volume = Managers.GData.player.BGMVol * 0.6f;
    }
    public float GetBGMVol()
    {
        return Managers.GData.player.BGMVol;
    }
    public void SetEffectVol(float vol) 
    {
        Managers.GData.player.EffectVol = vol;
        _audioSource[(int)Define.Sound.Effect].volume = Managers.GData.player.EffectVol * 0.8f;
    }
    public float GetEffectVol()
    {
        return Managers.GData.player.EffectVol;
    }

}
