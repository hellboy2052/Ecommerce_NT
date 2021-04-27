import React from "react";
import {
    Table,
    
  } from "reactstrap";
export default function UserList(props) {
  return (
    <Table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Name</th>
          <th scope="col">FullName</th>
        </tr>
      </thead>
      <tbody>
        {props.list &&
          props.list.map((item) => {
            return (
              <tr>
                <th scope="row">{item.userID}</th>
                <td>{item.email}</td>
                <td>{item.fullname}</td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
}
