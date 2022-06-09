import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";
import ChangeAchievement from "../ChangeAchievement/ChangeAchievement";

function AchievementData() {
	// AchivementData data
	const [achievementData, setAchievementData] = useState([]);
	const [achievement, setAchievement] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Name: false,
		Description: false,
		RewardExperience: false,
		ImgName: false
	});

	// Get achivement data
	function getAchievementData() {
		fetch(constants.API_URL + "AchievementData")
			.then(res => res.json())
			.then(data => setAchievementData(data));
	}

	// onLoad function
	useEffect(() => {
		getAchievementData();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setAchievement({
			Id: undefined,
			Name: "",
			Description: "",
			RewardExperience: 0,
			ImgName: ""
		});
	}

	// Show edit modal function
	function editModalShow(achievement) {
		editHandleShow();
		setAchievement(achievement);
	}

	// Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `AchievementData/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getAchievementData();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// Seact by name
	function searchName(name) {
		let arr = [];
		for (let i = 0; i < achievementData.length; i++) {
			if (achievementData[i].Name.match(name)) {
				arr.push(achievementData[i]);
			}
		}
		setAchievementData(arr);
	}

	// Sort functions
	function sortByName() {
		if (filterParameters.Name) {
			achievementData.sort((a, b) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Name: false,
				Description: filterParameters.Description,
				RewardExperience: filterParameters.RewardExperience,
				ImgName: filterParameters.ImgName
			});
		} else {
			achievementData.sort((b, a) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Name: true,
				Description: filterParameters.Description,
				RewardExperience: filterParameters.RewardExperience,
				ImgName: filterParameters.ImgName
			});
		}
	}
	function sortByRewardExperience() {
		if (filterParameters.RewardExperience) {
			achievementData.sort((a, b) =>
				a.RewardExperience > b.RewardExperience ? 1 : b.RewardExperience > a.RewardExperience ? -1 : 0
			);
			setFilterParameters({
				Name: filterParameters.Name,
				Description: filterParameters.Description,
				RewardExperience: false,
				ImgName: filterParameters.ImgName
			});
		} else {
			achievementData.sort((a, b) =>
				a.RewardExperience < b.RewardExperience ? 1 : b.RewardExperience < a.RewardExperience ? -1 : 0
			);
			setFilterParameters({
				Name: filterParameters.Name,
				Description: filterParameters.Description,
				RewardExperience: true,
				ImgName: filterParameters.ImgName
			});
		}
	}

	return (
		<div className="AchievementData">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new achievement
				</Button>
			</div>
			{/* Search */}
			<div className="container mt-5">
				<InputGroup className="mb-3">
					<FormControl
						aria-label="Default"
						placeholder="Search"
						value={search}
						onChange={e => setSearch(e.target.value)}
						aria-describedby="inputGroup-sizing-default"
					/>
					<Button onClick={() => searchName(search)}>Search</Button>
					<Button onClick={() => getAchievementData()}>Cancel</Button>
				</InputGroup>
			</div>
			{/* Create new item modal */}
			<Modal size="lg" centered show={addShow} onHide={addHandleClose}>
				<ChangeAchievement
					state={addHandleClose}
					achievement={achievement}
					setAchievement={setAchievement}
					getAchievementData={getAchievementData}
					method="POST"
					title="Add new achievement"
				/>
			</Modal>
			{/* Main table */}
			<div className="container mt-5">
				<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
					<thead>
						<tr>
							<th>
								Name
								<Button className="btn-light" onClick={() => sortByName()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Description
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								RewardExperience
								<Button className="btn-light" onClick={() => sortByRewardExperience()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								ImgName
								<Button className="btn-light">
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{achievementData.map(e => (
							<tr key={e.Id}>
								<td>{e.Name}</td>
								<td className="overflow-auto">{e.Description}</td>
								<td>{e.RewardExperience}</td>
								<td>
									<img src={e.ImgName} alt="img" width="100px" />
								</td>
								<td>
									<Button onClick={() => editModalShow(e)} variant="outline-dark">
										Edit
									</Button>
									<Button onClick={() => deleteClick(e.Id)} variant="outline-danger">
										Delete
									</Button>
								</td>
							</tr>
						))}
					</tbody>
				</Table>
			</div>
			{/* Edit item modal */}
			<Modal size="lg" centered show={editShow} onHide={editHandleClose}>
				<ChangeAchievement
					state={editHandleClose}
					achievement={achievement}
					setAchievement={setAchievement}
					getAchievementData={getAchievementData}
					method="PUT"
					title="Edit achievement"
				/>
			</Modal>
		</div>
	);
}

export default AchievementData;
