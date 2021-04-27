import api from '../api'
import {CATEGORY_LIST} from "../contains/category";
export const get_category_list = () => async (dispatch) => {
    try {
        const data = await api.Category.getAllCategory();

        dispatch({
            type: CATEGORY_LIST,
            payload: data,
        });

    } catch (error) {
        console.log(error);
    }
};
