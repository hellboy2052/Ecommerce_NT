import React,{useState,useEffect} from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import { post_images } from "../../actions/images";
import { useSelector, useDispatch } from "react-redux";
import { update_category } from "../../actions/category";

import { Link } from 'react-router-dom';
const UpdateCategory = ({match}) => {
  const {id}=match.params;
    const [category, setCategory] = useState({id:id});
  const postCategory = async() => {
    dispatch(update_category(category))
console.log(category)
  };
      const dispatch = useDispatch();
  
 
  return (
    <div>
       <Form>
                <FormGroup>
                  <Label for="exampleEmail">Name</Label>
                  <Input
                    type="text"
                    name="name"
                    id="exampleEmail"
                    placeholder="Name"
                    onChange={(e)=>setCategory({...category,name:e.target.value})}
                  />
                
                  <FormText color="muted">
                    This is some placeholder block-level help text for the above
                    input. It's a bit lighter and easily wraps to a new line.
                  </FormText>
                </FormGroup>


              </Form>
             {/* <Link to='/product'> </Link> */}
             {/* <Link to='/category'></Link> */}
             <Button color="success" onClick={()=>{postCategory()}}>Create</Button>
    </div>
  );
}

export default UpdateCategory;