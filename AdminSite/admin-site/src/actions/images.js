import api from '../api'
import {POST_IMAGES} from "../contains/images";
export const post_images = (image) => async (dispatch) => {
    try{
        const data=await api.Images.postImages(image);
        dispatch({
            type: POST_IMAGES,
            payload:data
        })
       
    }catch(error){
        console.log(error);
    }
};
