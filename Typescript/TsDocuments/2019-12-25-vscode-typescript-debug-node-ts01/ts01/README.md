# 使用vscode调试typescript
参考：
https://zhuanlan.zhihu.com/p/61583462
https://stackoverflow.com/questions/44611526/how-to-fix-cannot-find-module-typescript-in-angular-4

## 开发 TypeScript
1、建立项目目录
使用以下命令创建项目的目录：

`mkdir ts3`
`cd ts3`
`mkdir src`
`mkdir dist`
建立好的目录如下：

ts3
├─dist
└─src
2、初始化 NPM
在项目的根目录下，执行下面的命令：

npm init -y
现在项目结构如下：

ts3
├─dist
└─src
└─package.json
3、安装 TypeScript
在项目的根目录下，执行下面的命令：

npm i -g typescript
npm link typescript

4、创建并配置 tsconfig.json
在项目的根目录下，执行下面的命令：

tsc --init
现在项目结构如下：

ts3
├─dist
└─src
└─package.json
└─tsconfig.json
在 tsconfig.json 中取消下面属性项的注释，并修改其属性的值：

这样设置之后，我们在 ./src 中编码 .ts 文件，.ts 文件编译成 .js 后，输出到 ./dist 中。
"outDir": "./dist",
"rootDir": "./src",
5、Hello Typescript
将下面代码复制到./src/index.ts中：

const hello: string = 'hello, Alan.Tang';
console.log(hello);
在项目的根目录下，执行下面的命令：

tsc 是编译命令，详情查看：https://www.tslang.cn/docs/handbook/typescript-in-5-minutes.html
tsc 的编译选项，详情查看：https://www.tslang.cn/docs/handbook/compiler-options.html
tsc
node ./dist/index.js
执行结果如下：

PS C:\Users\Alan\TestCode\ts3> tsc
PS C:\Users\Alan\TestCode\ts3> node ./dist/index.js
hello, Alan.Tang


## 调试 TypeScript
如何 F5 开始调试 TypeScript ，并且还具备断点调试功能，答案是，使用 TS-node。
详情查看：https://github.com/TypeStrong/ts-node
在项目的根目录下，执行下面的命令：

npm install -D ts-node
在 VScode 调试中，添加配置：

{
  // 使用 IntelliSense 了解相关属性。 
  // 悬停以查看现有属性的描述。
  // 欲了解更多信息，请访问: https://go.microsoft.com/fwlink/?linkid=830387
  "version": "0.2.0",
  "configurations": [
    {
      "type": "node",
      "request": "launch",
      "name": "Launch Program",
      "runtimeArgs": [
        "-r",
        "ts-node/register"
      ],
      "args": [
        "${workspaceFolder}/src/index.ts"
      ]
    }
  ]
}