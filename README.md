# Don't cross the line(가명)
게임팀 IXTJ 프로젝트

## TAG 정리

| 태그     | 설명                                 |
|---------|---------------------------------------|
| feat    | 새로운 코드 추가                      |
| fix     | 문제점 수정                           |
| refact  | 코드 리팩토링                         |
| comment | 주석 추가(코드 변경X) 혹은 오타 수정  |
| docs    | README와 같은 문서 수정               |
| art     | 아트 에셋 추가                       |
| merge   | merge                                 |
| rename  | 파일, 폴더명 수정 혹은 이동           |
| chore   | 그 외 패키지 추가, 설정 변경 등        |

</br>

## Branch Name Convention

```
(TAG)/(주요내용)/(있다면 ISSUE NUMBER)

ex)
feat/player/#99
chore/package
```

</br>

## Commit Convention
```
(TAG)(있다면 ISSUE NUMBER) : 제목, 이때 영어라면 제일 앞 문자는 대문자로 시작
ex)
feat(#123) : A 기능을 구현하였다.

- A.cs 수정
- 그 외 comment 들

---

chore : A 패키지 추가
```

</br>

## PR Merge Convention

```
title: (TAG)/(ISSUE NUMBER) (PR NUMBER)
ex) FEAT/35 (#40)
```

</br>
