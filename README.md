# PROJECT WEBSITE PROGRAMING - ASP.NET MVC - YEAR 2

Name of the project: *Website xem phim Twoone*

## Abstract
- Website watching movie Twoone with up to **4000 films** and many interesting functions. We use asp.net MVC 5 technology to build the website. 
  The website has some layouts look like this:

- Here is short demo about Twoone website. [Click here to see full!!!](aa)

  <video src="source\short demo.mp4" style="zoom:50%;"></video>
## Idea

### Objects use

- User: watch movie, comment, update premium edit profile, register, login, forgot password, search.
- Admin: 
    - Management: film, director, actor, hashtags, genres, licenses.
    - Statistic: revenue month, revenue year, user subscribe month, user subscribe year, number of subscribers.

### Entity relationship diagram(ERD)

<img src="source\ERD.png" style="zoom:60%;" alt ="ERD"/>

### Use case summary

<img src="source\Usecasetq.png" style="zoom:60%" alt="usecase" />

## Requirements
- C# 
- Entity Framework (code first)
- Asp.net MCV 5
- Other library: pagelist, md5, mail, jquery, bootstrap
## Database (MSSQL)

### Diagram

<img src="source\sql.png" style="zoom:60%;" alt="sql diagram"/>

You can see more at full report in source folder or 

[**Click here!!!**]: source/fullreport.pdf	"Click here!!!"



#### Interface and Function of website

##### Home page

##### Login & Register & Reset Password

| Login                                                        | Register                                                     | Reset password                                               |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="source\login.png" alt="login" style="zoom:80%;" /> | <img src="source\register.png" alt="register" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_User_ForgotPassword_class=small.png" alt="twoonez-001-site1.itempurl.com_User_ForgotPassword_class=small" style="zoom:80%;" /> |

##### List film & actor

| List film - type list                                                                                                                                                          | List film - type grid                                                                                                                   | List actor                                                                                                                    |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------|
| <img src="source\twoonez-001-site1.itempurl.com_Movie_SearchByFilter_Grid=False.png" alt="twoonez-001-site1.itempurl.com_Movie_SearchByFilter_Grid=False" style="zoom:50%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Movie_SearchByFilter.png" alt="twoonez-001-site1.itempurl.com_Movie_SearchByFilter"  /> | <img src="source\twoonez-001-site1.itempurl.com_Movie_ActorGrid.png" alt="twoonez-001-site1.itempurl.com_Movie_ActorGrid"  /> |

#### Movie Details

| film information                                                                                                           | similar film                                                                                                                       |
|----------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------|
| ![twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262](source\twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262.png) | ![twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262 (2)](source\twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262 (2).png) |

##### Watching movie

##### <img src="source\twoonez-001-site1.itempurl.com_Movie_WatchingMovie_3262.png" alt="twoonez-001-site1.itempurl.com_Movie_WatchingMovie_3262" style="zoom: 50%;" />

#### Prerium

| Type of service                                                                                                      | type of payment                                                                                                                                  |
|----------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| ![twoonez-001-site1.itempurl.com_Payment_PaymentPage](source\twoonez-001-site1.itempurl.com_Payment_PaymentPage.png) | ![twoonez-001-site1.itempurl.com_Payment_ChonPhuongThucThanhToan_1](source\twoonez-001-site1.itempurl.com_Payment_ChonPhuongThucThanhToan_1.png) |

##### About us & Page not found

| About us                                                           | 404 page                                                                                                                                                                                                           |
|--------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <img src="source\about us.png" alt="about us" style="zoom:40%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Movie_PageNotFound_aspxerrorpath=_Movie_ActorList.png" alt="twoonez-001-site1.itempurl.com_Movie_PageNotFound_aspxerrorpath=_Movie_ActorList" style="zoom:50%;" /> |

#### User

| uSER PROFILE                                                                                               | FILM RATED                                                                                                                             | CHANGE PASS                                                                                                                                |
|------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| ![twoonez-001-site1.itempurl.com_User_ShowInfor](source\twoonez-001-site1.itempurl.com_User_ShowInfor.png) | ![twoonez-001-site1.itempurl.com_User_ShowInfor_mode=favorite](source\twoonez-001-site1.itempurl.com_User_ShowInfor_mode=favorite.png) | ![twoonez-001-site1.itempurl.com_User_ShowInfor_mode=changepass](source\twoonez-001-site1.itempurl.com_User_ShowInfor_mode=changepass.png) |

##### Admin

| Login                                                                                                                                          | Forget password                                                                                                                                                  |
|------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ![twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Login](source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Login.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_ForgotPassword](source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_ForgotPassword.png) |

##### Home Page Admin

![twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Adm_Home](source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Adm_Home.png)

##### 8 function management (C-R-U-D)

example: Film Management

| List film                                                                                                                  | Delete                                                                                                                                             | Create                                                                                                                                   | Edit                                                                                                                                           |
|----------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------|
| ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim](source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Delete_3262](source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Delete_3262.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Create](source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Create.png) | ![twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Edit_3262](source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Edit_3262.png) |

#### Statical

<img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Statistical.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Statistical" style="zoom:50%;" />

Above is all of function we made

Any question you can contact with us

email: vothuongtruongnhon2002@gmail.com

Author:

| leader                | member       | member         |
|-----------------------|--------------|----------------|
| Võ Thương Trường Nhơn | Phạm Đức Tài | Phạm Hồng Thái |

