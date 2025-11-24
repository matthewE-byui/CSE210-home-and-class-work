using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words = new List<Word>();
    private static Random _rnd = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        string[] splitWords = text.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);

        foreach (string w in splitWords)
        {
            _words.Add(new Word(w));
        }
    }

    public void HideRandomWords(int count)
    {
        // Build list of visible word indices so we don't repeatedly hide already-hidden words
        List<int> visibleIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
                visibleIndices.Add(i);
        }

        if (visibleIndices.Count == 0)
            return;

        // Hide up to `count` distinct visible words
        for (int i = 0; i < count && visibleIndices.Count > 0; i++)
        {
            int pick = _rnd.Next(visibleIndices.Count);
            int index = visibleIndices[pick];
            _words[index].Hide();
            visibleIndices.RemoveAt(pick);
        }
    }

    public string GetDisplayText()
    {
        string display = _reference.GetDisplayText() + "\n\n";

        foreach (Word w in _words)
        {
            display += w.GetDisplayText() + " ";
        }

        return display;
    }

    public bool AllWordsHidden()
    {
        foreach (Word w in _words)
        {
            if (!w.IsHidden())
                return false;
        }
        return true;
    }
}
