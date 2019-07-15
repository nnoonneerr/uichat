using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

/// an input textfield that will automatically clear itself when submitting
public class TextComposer : StatefulWidget
{
    public readonly Action<string> OnSubmit;

    public TextComposer(Action<string> onSubmit = null)
    {
        this.OnSubmit = onSubmit;
    }

    public override State createState()
    {
        return new TextComposerState();
    }
}

public class TextComposerState : State<TextComposer>
{
    private TextEditingController _textController = new TextEditingController();
    private FocusNode _textFocus = new FocusNode();

    public override Widget build(BuildContext context)
    {
        return new Container(
            margin: EdgeInsets.symmetric(horizontal : 8.0f),
            child : new Row(
                children: new List<Widget>
                {
                    /// text input
                    new Flexible(
                        child: new TextField(
                            controller : _textController,
                            decoration : InputDecoration.collapsed(hintText: "Send a message"),
                            onSubmitted : this.OnSend,
                            focusNode : _textFocus
                        )
                    ),
                    /// the Send button
                    new Container(
                        margin: EdgeInsets.symmetric(horizontal : 4.0f),
                        child : new IconButton(
                            icon: new Icon(Icons.send),
                            onPressed: () => this.OnSend(_textController.text))
                    )
                }
            )
        );
    }
    private void OnSend(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        _textController.clear();

        /// make sure the textfield is still focus after sending in case the user used the return key to send the message
        FocusScope.of(context).requestFocus(_textFocus);

        widget.OnSubmit?.Invoke(text);
    }
}