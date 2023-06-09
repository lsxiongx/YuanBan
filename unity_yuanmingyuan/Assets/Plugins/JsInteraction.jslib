mergeInto(LibraryManager.library, {
  HelloPlatform : function () {
    if (/iPhone|iPad|iPod|micromessenger|Android/i.test(navigator.userAgent)) {
        return false;
      } 
      else {
        return true;
      }
  },
  GetNpcId : function (strData) {
    var id=UTF8ToString(strData);
    console.log(strData);
    try{
        AreaDetect(strData);
    }catch(err){
        console.log(err)
    };
  },
  OnclickBanner : function (strBannerID,message) {
      var bannerID=UTF8ToString(strBannerID);
      var Message=UTF8ToString(message);
      try{
          clickBanner(bannerID,Message);
      }catch(err){
          console.log(err)
      };
    },
    OnclickButton : function (strButtonID,message) {
        var buttonID=UTF8ToString(strButtonID);
        var Message=UTF8ToString(message);
        try{
            clickButton(buttonID,Message);
        }catch(err){
            console.log(err)
        };
      },
    GetImageType : function (imageUrl,imagetype,imageaction) {
    var strUrl=UTF8ToString(imageUrl);
    var strtype=UTF8ToString(imagetype);
    var straction=UTF8ToString(imageaction);
    try{
        ImageDetect(strUrl,strtype,straction);
    }catch(err){
        console.log(err)
    };
    },
    Test : function (strDialogue) {
    var Dialogues = UTF8ToString(strDialogue);
    //myunityInstance.SendMessage('WebController', 'PlayerTransfer', Dialogues);
    },
});
