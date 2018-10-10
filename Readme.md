
WeChat History Extractor
============

使用基于 Appium 的界面自动化技术，提取微信"合并聊天记录"消息中的内容。


## 为了运行这个程序，你需要：

* Mac & WeChat for Mac
* nodejs & npm
* Appium
* Appium Mac driver
* .NET Core
* 在系统偏好设置里，将 "Appium for Mac" 添加到"允许使用 Accessibility API 控制计算机"的列表中


## 运行过程

 1. 运行 appium
 2. 打开微信，手动点击一个聊天记录消息
 3. 运行本程序
 


## 项目状态

当前处于 PoC 阶段，暂时只支持文本消息的导出。

接下来的开发计划：

1. 支持图片消息的导出
1. 支持高清图片消息的导出
1. 支持视频消息的导出
1. 将实现代码换为基于 Node.js 实现，而非 .NET Core（这样可以减少使用环境的依赖复杂性）


