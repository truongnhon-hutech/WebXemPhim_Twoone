# PROJECT WEBSITE PROGRAMING - ASP.NET MVC - YEAR 2

Name of the project: *Website xem phim Twoone*

## Abstract
- Website watching movie Twoone with up to **4000 films** and many interesting functions. We use asp.net MVC 5 technology to build the website. 
  The website has some layouts look like this:

- Here is short demo about Twoone website. [Click here to see full!!!](https://www.youtube.com/watch?v=pwxriq0qSIQ)

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

| film information                                             | similar film                                                 |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="source\twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262.png" alt="twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262 (2).png" alt="twoonez-001-site1.itempurl.com_Movie_MovieDetail_3262 (2)" style="zoom:80%;" /> |

##### Watching movie

##### <img src="source\twoonez-001-site1.itempurl.com_Movie_WatchingMovie_3262.png" alt="twoonez-001-site1.itempurl.com_Movie_WatchingMovie_3262" style="zoom: 50%;" />

#### Premium

| Type of service                                              | type of payment                                              |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="source\twoonez-001-site1.itempurl.com_Payment_PaymentPage.png" alt="twoonez-001-site1.itempurl.com_Payment_PaymentPage" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Payment_ChonPhuongThucThanhToan_1.png" alt="twoonez-001-site1.itempurl.com_Payment_ChonPhuongThucThanhToan_1" style="zoom:80%;" /> |

##### About us & Page not found

| About us                                                           | 404 page                                                                                                                                                                                                           |
|--------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <img src="source\about us.png" alt="about us" style="zoom:40%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Movie_PageNotFound_aspxerrorpath=_Movie_ActorList.png" alt="twoonez-001-site1.itempurl.com_Movie_PageNotFound_aspxerrorpath=_Movie_ActorList" style="zoom:50%;" /> |

#### User

| uSER PROFILE                                                 | FILM RATED                                                   | CHANGE PASS                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="source\twoonez-001-site1.itempurl.com_User_ShowInfor.png" alt="twoonez-001-site1.itempurl.com_User_ShowInfor" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_User_ShowInfor_mode=favorite.png" alt="twoonez-001-site1.itempurl.com_User_ShowInfor_mode=favorite" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_User_ShowInfor_mode=changepass.png" alt="twoonez-001-site1.itempurl.com_User_ShowInfor_mode=changepass" style="zoom:80%;" /> |

##### Admin

| Login                                                        | Forget password                                              |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Login.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Login" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_ForgotPassword.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_ForgotPassword" style="zoom:80%;" /> |

##### Home Page Admin

<img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Adm_Home.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Adm_Home" style="zoom:80%;" />

##### 8 function management (C-R-U-D)

example: Film Management

| List film                                                    | Delete                                                       | Create                                                       | Edit                                                         |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| <img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_Phim" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Delete_3262.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Delete_3262" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Create.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Create" style="zoom:80%;" /> | <img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Edit_3262.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_Phim_Edit_3262" style="zoom:80%;" /> |

#### Statical

<img src="source\twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Statistical.png" alt="twoonez-001-site1.itempurl.com_Administrator_Adm_TrangChu_Statistical" style="zoom:50%;" />

Above is all of function we made

Any question you can contact with us

email: vothuongtruongnhon2002@gmail.com

Author:

| leader                | member       | member         |
|-----------------------|--------------|----------------|
| Võ Thương Trường Nhơn | Phạm Đức Tài | Phạm Hồng Thái |

