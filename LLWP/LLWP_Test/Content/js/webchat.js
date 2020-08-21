(async function () {
    window.WebChat.renderWebChat({
        directLine: window.WebChat.createDirectLine({ token }),
        styleOptions: {
            rootHeight: '100%',
            rootWidth: '100%'
        }
    }, document.getElementById('webchat'));
})()

// "styleSet" is a set of CSS rules which are generated from "styleOptions"
const styleSet = window.WebChat.createStyleSet({
    bubbleBackground: '#FFF9DB',
    bubbleBorderRadius: 10,
    bubbleFromUserBorderRadius: 10,
    bubbleFromUserBackground: '#F5F1ED',
    botAvatarImage: '../Content/images/LLWP_LOGO.png',
    botAvatarBackgroundColor: 'White',
    botAvatarInitials: '',
    userAvatarImage: '../Content/images/LLWP_LOGO2.png',
    userAvatarBackgroundColor: 'White',
    userAvatarInitials: '',
    bubbleBorderColor: 'transparent',
    bubbleFromUserBorderColor: 'transparent',
});

// After generated, you can modify the CSS rules
styleSet.textContent = {
    ...styleSet.textContent,
    fontFamily: "'Comic Sans MS', 'Arial', sans-serif",


};

window.WebChat.renderWebChat(
    {
        directLine: window.WebChat.createDirectLine({
            secret: 'PR139WlkAhQ.Toi2U8ZysOjcUpyyMSTcsgzae2zvbbReBvgCCFFplns'
        }),

        // Passing 'styleSet' when rendering Web Chat
        styleSet
    },
    document.getElementById('webchat')
);

function addNotification() {

    var notification = $('.bell-notification');

    if (!$(notification).hasClass('new-not')) {

        $('.bell-top').addClass('bell-top-anim');
        $('.bell-bot').addClass('bell-bot-anim');

        setTimeout(function () {
            $(notification).addClass('new-not');
        }, 800);

        $(notification).html(parseInt($(notification).html(), 10) + 1);

    } else {
        $('.bell-top').removeClass('bell-top-anim');
        $('.bell-bot').removeClass('bell-bot-anim');
        $(notification).removeClass('new-not');


    }

} 