mergeInto(LibraryManager.library, {
  alert: function (msg) {
     window.alert(UTF8ToString(msg));
  },
});