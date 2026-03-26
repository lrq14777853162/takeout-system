# 外卖平台 (FoodDelivery Platform)

基于 **ABP vNext 8.x (.NET 8)** 后端 + **UniApp (Vue3)** 前端的全功能外卖平台，支持用户端、骑手端、商家端和后台管理端。

## 📋 技术栈

### 后端
| 技术 | 版本 | 用途 |
|------|------|------|
| .NET | 8.0 | 运行时 |
| ABP vNext | 8.2.x | 应用框架 |
| Entity Framework Core | 8.x | ORM |
| MySQL | 8.0 | 主数据库 |
| Redis | 7.x | 缓存 |
| RabbitMQ | 3.x | 消息队列 |
| SignalR | 内置 | 实时通信 |
| JWT | 内置 | 身份认证 |
| Swagger | 内置 | API 文档 |

### 前端
| 技术 | 版本 | 用途 |
|------|------|------|
| UniApp | 3.x | 跨端框架 |
| Vue | 3.x | UI框架 |
| Pinia | 2.x | 状态管理 |
| @microsoft/signalr | 8.x | 实时通信客户端 |
| dayjs | 1.x | 日期处理 |

## 📁 项目结构

```
takeout-system/
├── backend/                          # 后端（ABP vNext）
│   ├── src/
│   │   ├── FoodDelivery.Domain.Shared/   # 枚举、常量、错误码
│   │   ├── FoodDelivery.Domain/          # 实体、聚合根、领域服务
│   │   ├── FoodDelivery.Application.Contracts/  # DTO、接口、权限
│   │   ├── FoodDelivery.Application/     # 应用服务实现
│   │   ├── FoodDelivery.EntityFrameworkCore/   # EF Core + 迁移
│   │   ├── FoodDelivery.HttpApi/         # Controller
│   │   └── FoodDelivery.HttpApi.Host/    # 启动宿主
│   └── test/
│       ├── FoodDelivery.Domain.Tests/
│       └── FoodDelivery.Application.Tests/
├── frontend/                         # 前端（UniApp Vue3）
│   └── src/
│       ├── api/       # API 接口层
│       ├── pages/     # 页面（用户/商家/骑手/管理员）
│       ├── components/ # 公共组件
│       ├── store/     # Pinia 状态管理
│       └── utils/     # 工具函数
├── docker-compose.yml
└── README.md
```

## 🔧 开发环境要求

- .NET SDK 8.0+
- Node.js 18+
- Docker & Docker Compose（可选，用于本地依赖服务）
- MySQL 8.0+（或使用 Docker）
- Redis 7+（或使用 Docker）

## 🚀 快速启动

### 方式一：使用 Docker Compose（推荐）

```bash
# 启动所有依赖服务（MySQL, Redis, RabbitMQ）
docker-compose up -d mysql redis rabbitmq

# 等待服务就绪后，运行后端
cd backend
dotnet restore
cd src/FoodDelivery.HttpApi.Host
dotnet run
```

### 方式二：本地启动

#### 后端

1. 配置数据库连接（修改 `backend/src/FoodDelivery.HttpApi.Host/appsettings.json`）：
```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Port=3306;Database=food_delivery;User Id=root;Password=yourpassword;"
  }
}
```

2. 运行数据库迁移：
```bash
cd backend/src/FoodDelivery.EntityFrameworkCore
dotnet ef migrations add Initial
dotnet ef database update
```

3. 启动 API：
```bash
cd backend/src/FoodDelivery.HttpApi.Host
dotnet run
```

API 将在 `http://localhost:5000` 启动，Swagger 文档：`http://localhost:5000/swagger`

#### 前端

```bash
cd frontend
npm install

# 开发模式（H5）
npm run dev:h5

# 微信小程序
npm run dev:mp-weixin
```

## 🏗️ 业务模块

### 用户端 (Customer)
- 🏠 首页：附近商家列表、搜索
- 🍽️ 商家详情：菜单、规格选择
- 🛒 购物车：加减商品、跨商家提示
- 📝 下单：地址选择、优惠券、备注
- 📦 订单管理：订单列表、详情、实时追踪
- ⭐ 评价：评分、图片上传
- 🏷️ 优惠券：领取、使用

### 商家端 (Merchant)
- 📊 工作台：今日数据、待处理订单
- 📋 订单管理：接单、拒单、查看详情
- 🛍️ 商品管理：CRUD、上下架
- 📂 分类管理
- ⚙️ 店铺设置：营业时间、配送范围
- 📈 数据统计
- 💰 财务管理

### 骑手端 (Rider)
- 🗺️ 工作台：在线/离线、接单
- 📍 配送：实时位置上报
- 📜 历史订单
- 💵 收入明细、提现

### 后台管理 (Admin)
- 📊 数据看板
- ✅ 商家审核/骑手审核
- 👤 用户管理
- 💹 财务管理
- 🎟️ 优惠券管理
- ⚙️ 系统配置

## 📡 实时通信

系统使用 **SignalR** 实现以下实时功能：
- 骑手位置实时更新
- 订单状态实时推送
- 新订单提醒（商家端）

Hub 地址：`ws://localhost:5000/hubs/order`

## 🔒 权限设计

| 角色 | 权限范围 |
|------|---------|
| Customer | 下单、查看自己的订单 |
| Merchant | 管理自己的商家、商品、订单 |
| Rider | 接单、更新位置 |
| Admin | 全部权限 |

## 📄 License

MIT