import React from 'react'
import Oidc from 'oidc-client'
export default function LoginCallBack() {
    var userManager=new Oidc.UserManager({userStore:new Oidc.WebStorageStateStore({store:window.localStorage}),})
    return (
        <div>
            {
                 userManager.signinCallback().then( res=>{
                    userManager.getUser()
                    .then(console.log(Oidc.UserManager))
                    .then(user=>user.profile.role==="admin"?window.location.href="http://localhost:3000/product":window.location.href="https://localhost:3000")
                })         
            }
        </div>
    )
}